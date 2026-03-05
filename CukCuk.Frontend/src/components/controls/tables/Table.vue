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
          >
            <div class="cell-content" :style="{ textAlign: header.align }">
              <!-- Slot cho header nếu cần custom (ví dụ checkbox header) -->
              <slot v-if="header.headerSlot" :name="`header-${header.key}`" :header="header">
                {{ header.label }}
              </slot>
              <template v-else>
                {{ header.label }}
              </template>
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
            <div class="cell-content" :style="{ textAlign: header.align }">
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
})
defineEmits(['row-dblclick', 'row-click', 'update:data'])

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
    zIndex: 5,
    background: '#fff',
    boxShadow: '2px 0 4px rgba(0,0,0,0.05)',
  }
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
  z-index: 2;
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
  padding: 0 12px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  word-break: break-all; /* Fallback in case the content is still too long */
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
  z-index: 10; /* Đảm bảo nằm trên các cột khác khi cuộn */
}

/* Khi hover vào row, cell action cũng đổi màu */
.content_body_table tr:hover .sticky-action-cell {
  background-color: transparent;
}

/* Header của cột action phải có z-index cao hơn header thường */
.sticky-action-header {
  z-index: 11;
}
</style>
