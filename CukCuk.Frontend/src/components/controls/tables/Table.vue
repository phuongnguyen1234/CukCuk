<template>
  <div class="content_body_table_wrapper">
    <table class="content_body_table" :class="{ 'has-vertical-borders': showVerticalBorders }">
      <thead>
        <tr>
          <th
            v-for="(header, colIndex) in headers"
            :key="colIndex"
            :style="{
              width: header.width,
              minWidth: header.minWidth,
              ...pinnedOffset(colIndex),
            }"
            class="table-header-cell"
            @click="onHeaderClick(header, $event)"
          >
            <div class="cell-content" :style="{ justifyContent: getFlexAlign(header.align) }">
              <div class="header-label" :style="{ textAlign: header.align }">
                <!-- Slot cho header nếu cần custom (ví dụ checkbox header) -->
                <slot v-if="header.headerSlot" :name="`header-${header.key}`" :header="header">
                  {{ header.label }}
                </slot>
                <template v-else>
                  {{ header.label }}
                </template>
              </div>
              <span v-if="activeFilterKeys.includes(header.key)" class="filter-indicator">
                <svg
                  width="12"
                  height="13"
                  viewBox="0 0 12 13"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    d="M11.3333 0H0.666667C0.489856 0 0.320286 0.0702379 0.195262 0.195262C0.0702379 0.320286 0 0.489856 0 0.666667V2.15133L0.00533326 2.3C0.0385388 2.74515 0.219709 3.16639 0.52 3.49667L3.33333 6.59067V12C3.33331 12.1056 3.35839 12.2098 3.4065 12.3038C3.45461 12.3978 3.52438 12.4791 3.61006 12.5409C3.69574 12.6027 3.79488 12.6432 3.8993 12.6592C4.00373 12.6751 4.11044 12.666 4.21067 12.6327L8.21067 11.2993L8.28267 11.2707C8.39748 11.2169 8.49458 11.1315 8.56259 11.0246C8.63061 10.9176 8.66671 10.7934 8.66667 10.6667V6.276L11.414 3.52933C11.5999 3.3435 11.7473 3.12285 11.8479 2.87999C11.9484 2.63714 12.0001 2.37685 12 2.114V0.666667C12 0.489856 11.9298 0.320286 11.8047 0.195262C11.6797 0.0702379 11.5101 0 11.3333 0Z"
                    fill="#245FDF"
                  />
                </svg>
              </span>
            </div>
          </th>
          <!-- Header cho cột action, không có text, chỉ để giữ chỗ và sticky -->
          <th v-if="$slots['row-actions']" class="sticky-action-header"></th>
        </tr>
      </thead>

      <!-- Standard Body -->
      <tbody>
        <tr
          v-for="(item, index) in data"
          :key="item[rowKey] || index"
          v-show="!rowFilterFn || rowFilterFn(item)"
          @dblclick="$emit('row-dblclick', item)"
          @click="$emit('row-click', item)"
        >
          <td
            v-for="(header, colIndex) in headers"
            :key="colIndex"
            :style="{
              width: header.width,
              minWidth: header.minWidth,
              ...pinnedOffset(colIndex),
            }"
          >
            <div class="cell-content" :style="{ justifyContent: getCellJustify(header.align) }">
              <!-- Custom slot -->
              <slot
                v-if="header.type === 'custom'"
                :name="header.key"
                :data="item"
                :index="index"
              ></slot>

              <!-- Date type -->
              <span v-else-if="header.type === 'date'">{{ item[header.key] }}</span>

              <!-- Number type -->
              <span v-else-if="header.type === 'number'">{{ item[header.key] }}</span>

              <!-- Currency type (Tiền tệ) -->
              <span v-else-if="header.type === 'currency'">{{
                formatCurrency(item[header.key])
              }}</span>

              <!-- Boolean type -->
              <div v-else-if="header.type === 'boolean'" class="boolean-cell">
                <div v-if="item[header.key]">
                  <svg
                    width="12"
                    height="9"
                    viewBox="0 0 12 9"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M1 4.33333L4.33333 7.66667L11 1"
                      stroke="#245FDF"
                      stroke-width="2"
                      stroke-linecap="round"
                      stroke-linejoin="round"
                    />
                  </svg>
                </div>
              </div>

              <!-- Default text -->
              <span v-else>{{ item[header.key] }}</span>

              <!-- Vị trí cũ của row-actions đã được xóa khỏi đây -->
            </div>
          </td>
          <!-- Cột action cố định bên phải -->
          <td v-if="$slots['row-actions']" class="sticky-action-cell">
            <div class="row-actions-wrapper">
              <slot name="row-actions" :data="item" :index="index"></slot>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { formatCurrency } from '@/utils/format-util.js'

const props = defineProps({
  headers: {
    type: Array,
    required: true,
  },
  data: {
    type: Array,
    required: true,
  },
  showVerticalBorders: {
    type: Boolean,
    default: false,
  },
  rowFilterFn: {
    type: Function,
    default: null,
  },
  rowKey: {
    type: String,
    default: 'id',
  },
  activeFilterKeys: {
    type: Array,
    default: () => [],
  },
})
const emit = defineEmits(['row-dblclick', 'row-click', 'update:data', 'header-click'])

function getWidthNumber(width) {
  if (!width) return 0
  return Number(String(width).replace('px', ''))
}

function pinnedOffset(colIndex) {
  const col = props.headers[colIndex]

  if (col?.pinned !== 'left') return {}

  let offset = 0

  for (let i = 0; i < colIndex; i++) {
    if (props.headers[i].pinned === 'left') {
      offset += getWidthNumber(props.headers[i].width)
    }
  }

  return {
    position: 'sticky',
    left: offset + 'px',
    zIndex: 'var(--z-index-table-pinned)',
    background: '#fff',
    boxShadow: '2px 0 4px rgba(0,0,0,0.05)',
  }
}

function onHeaderClick(header, event) {
  // Chỉ emit cho các cột có thể lọc được (nếu có cờ `filterable`)
  if (header.filterable !== false) {
    emit('header-click', header, event)
  }
}

function getFlexAlign(align) {
  if (align === 'right') return 'flex-end'
  if (align === 'center') return 'center'
  return 'space-between'
}

function getCellJustify(align) {
  if (align === 'right') return 'flex-end'
  if (align === 'center') return 'center'
  return 'flex-start'
}
</script>

<style scoped>
.content_body_table_wrapper {
  background-color: #fff;
  flex: 1;
  overflow: auto;
  border-left: 1px solid #e0e6ec;
  border-right: 1px solid #e0e6ec;
}

.content_body_table {
  width: 100%;
  border-collapse: separate;
  border-spacing: 0;
  table-layout: fixed;
}

.content_body_table th {
  position: sticky;
  top: 0;
  background-color: #fafafa;
  z-index: var(--z-index-table-header);
  height: 36px;
  padding: 0; /* Padding is now on the inner cell-content */
  text-align: left;
  font-weight: 600;
  font-size: 13px;
  border-bottom: 1px solid #e9eaeb;
  border-top: 1px solid #e9eaeb;
  box-sizing: border-box;
}

.content_body_table td {
  height: 36px;
  padding: 0; /* Padding is now on the inner cell-content */
  border-bottom: 1px solid #e9eaeb;
  font-size: 14px;
  color: #111;
  background-color: #fff;
  transition: background-color 0.2s ease;
  box-sizing: border-box;
}

.cell-content {
  display: flex;
  align-items: center;
  padding: 0 12px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  word-break: break-all; /* Fallback in case the content is still too long */
}

.table-header-cell {
  cursor: pointer;
}

.table-header-cell:hover {
  background-color: #f2f9ff;
}

.header-label {
  flex-grow: 1;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.filter-indicator {
  margin-left: 4px;
  flex-shrink: 0;
  display: flex;
  align-items: center;
}

.boolean-cell {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
}

.content_body_table.has-vertical-borders th:not(:last-child),
.content_body_table.has-vertical-borders td:not(:last-child) {
  border-right: 1px solid #e9eaeb;
}

.content_body_table tr:hover td {
  background-color: #f0f6fe;
}

.row-actions-wrapper {
  display: none;
  position: absolute;
  right: 0;
  top: 0;
  align-items: center;
  padding-right: 12px; /* Cách lề phải một chút cho đẹp */
  height: 100%;
  width: max-content; /* Bắt buộc mở rộng theo nội dung, ngăn ButtonGroup bị xuống dòng */
}

.content_body_table tr:hover .row-actions-wrapper {
  display: flex;
}

/* --- Kiểu cho cột action cố định --- */
.sticky-action-header,
.sticky-action-cell {
  position: sticky;
  right: 0;
  width: 0 !important; /* Không chiếm diện tích ngang */
  padding: 0 !important; /* Xóa padding để không bị đẩy ra */
  border: none;
  background: transparent; /* Trong suốt để đúng chất overlay */
  overflow: visible;
}

/* Ghi đè overflow:hidden cho ô action để các nút không bị cắt.
 * Selector này có độ ưu tiên cao hơn (.content_body_table td) nên sẽ được áp dụng. */
.content_body_table .sticky-action-cell {
  overflow: visible;
}

.sticky-action-cell {
  z-index: var(--z-index-table-action); /* Đảm bảo nằm trên các cột khác khi cuộn */
}

/* Khi hover vào row, cell action cũng đổi màu */
.content_body_table tr:hover .sticky-action-cell {
  background-color: transparent;
}

/* Header của cột action phải có z-index cao hơn header thường */
.sticky-action-header {
  z-index: calc(var(--z-index-table-action) + 1);
}
</style>
