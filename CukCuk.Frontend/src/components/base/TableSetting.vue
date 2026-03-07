<template>
  <BaseModal :visible="props.visible" width="700px" @update:visible="close">
    <template #title>Thiết lập bảng</template>
    <div class="table-setting-container">
      <Searchbar v-model="searchQuery" placeholder="Tìm kiếm" class="search-bar" />
      <div class="table-wrapper">
        <table class="custom-settings-table">
          <thead>
            <tr>
              <th style="width: 40px; text-align: center">
                <Checkbox v-model="areAllVisible" />
              </th>
              <th style="text-align: left">Tên cột</th>
              <th style="width: 150px; text-align: right">Độ rộng cột</th>
              <th style="width: 100px; text-align: center">Chức năng</th>
            </tr>
          </thead>
          <draggable
            v-model="filteredColumns"
            tag="tbody"
            item-key="key"
            handle=".drag-handle"
            ghost-class="sortable-ghost"
          >
            <template #item="{ element: column }">
              <tr>
                <td style="text-align: center">
                  <Checkbox
                    :model-value="column.visible"
                    @update:model-value="(val) => handleVisibilityChange(column, val)"
                  />
                </td>
                <td>{{ column.label }}</td>
                <td>
                  <Input
                    v-model="column.width"
                    :rules="['number']"
                    placeholder="Tự động"
                    style="text-align: right"
                    @blur="validateWidth(column)"
                  />
                </td>
                <td class="actions-cell">
                  <ButtonIcon
                    variant="text"
                    :title="column.pinned ? 'Bỏ ghim' : 'Ghim cột'"
                    @click="togglePin(column)"
                  >
                    <!-- Pinned Icon -->
                    <svg
                      v-if="column.pinned"
                      width="10"
                      height="15"
                      viewBox="0 0 10 15"
                      fill="none"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        d="M7.41667 0.75C7.58659 0.750188 7.75002 0.815253 7.87358 0.931899C7.99714 1.04855 8.07149 1.20797 8.08145 1.3776C8.09141 1.54722 8.03622 1.71425 7.92716 1.84455C7.8181 1.97486 7.6634 2.0586 7.49467 2.07867L7.41667 2.08333V5.25933L8.67933 7.78533C8.7142 7.85446 8.73676 7.92913 8.746 8.006L8.75 8.08333V9.41667C8.74998 9.57996 8.69003 9.73756 8.58152 9.85958C8.47302 9.9816 8.3235 10.0596 8.16133 10.0787L8.08333 10.0833H5.41667V12.75C5.41648 12.9199 5.35141 13.0834 5.23477 13.2069C5.11812 13.3305 4.9587 13.4048 4.78907 13.4148C4.61944 13.4247 4.45241 13.3696 4.32211 13.2605C4.19181 13.1514 4.10807 12.9967 4.088 12.828L4.08333 12.75V10.0833H1.41667C1.25338 10.0833 1.09578 10.0234 0.973752 9.91486C0.851729 9.80635 0.773771 9.65683 0.754667 9.49467L0.75 9.41667V8.08333C0.750091 8.00603 0.763626 7.92933 0.79 7.85667L0.820667 7.78533L2.08333 5.258V2.08333C1.91341 2.08314 1.74998 2.01808 1.62642 1.90143C1.50286 1.78479 1.42851 1.62536 1.41855 1.45574C1.40859 1.28611 1.46378 1.11908 1.57284 0.988778C1.6819 0.858476 1.8366 0.774736 2.00533 0.754667L2.08333 0.75H7.41667Z"
                        fill="#245FDF"
                        stroke="#245FDF"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                      />
                    </svg>
                    <!-- Unpinned Icon -->
                    <svg
                      v-else
                      width="12"
                      height="12"
                      viewBox="0 0 12 12"
                      fill="none"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <path
                        d="M7.75 1.08333L5.08333 3.75L2.41667 4.75L1.41667 5.75L6.08333 10.4167L7.08333 9.41667L8.08333 6.75L10.75 4.08333M3.75 8.08333L0.75 11.0833M7.41667 0.75L11.0833 4.41667"
                        stroke="#245FDF"
                        stroke-width="1.5"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                      />
                    </svg>
                  </ButtonIcon>
                  <GripIcon class="drag-handle" />
                </td>
              </tr>
            </template>
          </draggable>
        </table>
      </div>
    </div>

    <template #footer>
      <div class="footer-actions">
        <Button variant="outline" @click="resetToDefault">Lấy lại mặc định</Button>
        <ButtonGroup>
          <Button variant="secondary" @click="close">Hủy</Button>
          <Button variant="primary" @click="save">Lưu</Button>
        </ButtonGroup>
      </div>
    </template>
  </BaseModal>
</template>

<script setup>
import { ref, watch, computed, onMounted } from 'vue'
import draggable from 'vuedraggable'
import BaseModal from '@/components/base/BaseModal.vue'
import Searchbar from '@/components/controls/inputs/Searchbar.vue'
import Checkbox from '@/components/controls/checkboxes/Checkbox.vue'
import Input from '@/components/controls/inputs/Input.vue'
import Button from '@/components/controls/buttons/Button.vue'
import ButtonGroup from '@/components/controls/buttons/ButtonGroup.vue'
import ButtonIcon from '@/components/controls/buttons/ButtonIcon.vue'
import GripIcon from '@/components/icons/GripIcon.vue'
import { useToast } from '@/utils/use-toast'

const props = defineProps({
  visible: Boolean,
  /**
   * Mảng các đối tượng cột.
   * Mỗi đối tượng nên có: key, label, width, visible, pinned
   */
  columns: {
    type: Array,
    default: () => [],
  },
})

const emit = defineEmits(['update:visible', 'save', 'reset'])

const { showToast } = useToast()
const searchQuery = ref('')
const localColumns = ref([])
const MAX_PINNED_COLUMNS = 5 // Giới hạn số cột có thể ghim

// Hàm khởi tạo dữ liệu từ props
function initData() {
  if (props.columns && props.columns.length > 0) {
    localColumns.value = JSON.parse(JSON.stringify(props.columns))
  }
}

// Watch thêm props.columns phòng trường hợp dữ liệu thay đổi từ cha khi modal đang mở
watch(() => props.columns, initData, { deep: true, immediate: true })

const filteredColumns = computed({
  get() {
    const query = searchQuery.value.toLowerCase()
    if (!query) {
      return localColumns.value
    }
    return localColumns.value.filter((col) => (col.label || '').toLowerCase().includes(query))
  },
  set(reorderedList) {
    // Nếu không có filter, danh sách được cập nhật là danh sách đầy đủ đã sắp xếp lại.
    if (!searchQuery.value) {
      localColumns.value = reorderedList
      return
    }

    // Nếu có filter, thực hiện việc hợp nhất phức tạp để duy trì thứ tự.
    const reorderedKeys = new Set(reorderedList.map((i) => i.key))
    const reorderedIterator = reorderedList[Symbol.iterator]()

    const newFullList = localColumns.value.map((originalItem) => {
      if (reorderedKeys.has(originalItem.key)) {
        // Vị trí này trong danh sách đầy đủ được chiếm bởi một mục đã lọc.
        // Thay thế nó bằng mục tiếp theo từ danh sách đã được sắp xếp lại.
        return reorderedIterator.next().value
      } else {
        // Mục này không thuộc bộ lọc, vì vậy nó giữ nguyên vị trí tương đối của mình.
        return originalItem
      }
    })

    localColumns.value = newFullList
  },
})

const currentlyVisibleInUI = computed(() => {
  // Bây giờ chỉ cần trả về danh sách đã lọc
  return filteredColumns.value
})

// Logic cho checkbox ở header để chọn/bỏ chọn tất cả
const areAllVisible = computed({
  get() {
    const list = currentlyVisibleInUI.value
    // Chỉ đúng khi danh sách hiển thị có item và tất cả item đó đều được check
    return list.length > 0 && list.every((c) => c.visible)
  },
  set(value) {
    // Chỉ thay đổi trạng thái của các item đang hiển thị trong UI
    currentlyVisibleInUI.value.forEach((c) => (c.visible = value))
  },
})

function handleVisibilityChange(column, isVisible) {
  if (isVisible) {
    // Nếu đang ẩn -> hiện: Luôn cho phép
    column.visible = true
  } else {
    // Nếu đang hiện -> ẩn: Cần kiểm tra kỹ
    // Giả lập danh sách cột hiển thị SAU KHI ẩn cột này
    const visibleColsAfterHide = localColumns.value.filter((c) => c.visible && c.key !== column.key)
    const pinnedColsAfterHide = visibleColsAfterHide.filter((c) => c.pinned).length

    // Logic: Nếu sau khi ẩn, số cột ghim chiếm TOÀN BỘ số cột hiển thị còn lại -> Chặn
    if (visibleColsAfterHide.length > 0 && pinnedColsAfterHide >= visibleColsAfterHide.length) {
      showToast('Không thể ẩn cột này. Cần ít nhất một cột không ghim để hiển thị đúng.', 'warning')
      return
    }

    column.visible = false
  }
}

function validateWidth(column) {
  const width = parseInt(column.width, 10)
  const minWidth = parseInt(column.minWidth, 10)

  // Nếu cột không có minWidth hợp lệ, không thực thi logic này.
  if (isNaN(minWidth)) {
    return
  }

  // Nếu giá trị nhập vào không phải là số (rỗng, chữ...) hoặc nhỏ hơn minWidth,
  // thì sẽ tự động đặt lại thành minWidth.
  if (isNaN(width) || width < minWidth) {
    column.width = minWidth
    // Chỉ hiện toast khi người dùng cố tình nhập một số nhỏ hơn, không phải khi họ xóa trống.
    if (!isNaN(width) && width > 0) {
      showToast(`Độ rộng tối thiểu cho cột "${column.label}" là ${minWidth}px.`, 'warning')
    }
  }
}

function togglePin(column) {
  if (column.pinned) {
    column.pinned = false
  } else {
    // Đếm số lượng cột đang ghim trên TOÀN BỘ danh sách (bao gồm cả cột ẩn)
    // để tránh lỗ hổng: ẩn cột đã ghim -> ghim cột mới -> hiện lại cột cũ
    const totalPinnedCount = localColumns.value.filter((c) => c.pinned).length

    // Kiểm tra 1: Không cho ghim quá giới hạn cứng
    if (totalPinnedCount >= MAX_PINNED_COLUMNS) {
      showToast(`Không thể ghim quá ${MAX_PINNED_COLUMNS} cột.`, 'warning')
      return
    }

    // Kiểm tra 2: Logic giữ lại ít nhất 1 cột không ghim (chỉ tính trên các cột hiển thị)
    const visibleCols = localColumns.value.filter((c) => c.visible)
    const visiblePinnedCount = visibleCols.filter((c) => c.pinned).length

    // Kiểm tra 2: Luôn giữ lại ít nhất một cột không ghim để cuộn (quan trọng khi số cột ít)
    if (visibleCols.length > 0 && visiblePinnedCount >= visibleCols.length - 1) {
      showToast(
        'Không thể ghim tất cả cột. Cần ít nhất một cột không ghim để hiển thị đúng.',
        'warning',
      )
      return
    }
    column.pinned = 'left'
  }
}

function close() {
  emit('update:visible', false)
}

function save() {
  // Duyệt qua tất cả các cột để validate lại độ rộng một lần nữa
  // Đảm bảo xử lý trường hợp người dùng sửa xong ấn Lưu ngay (chưa kịp blur)
  localColumns.value.forEach((col) => validateWidth(col))

  emit('save', localColumns.value)
  close()
}

function resetToDefault() {
  emit('reset')
}
</script>

<style scoped>
.table-setting-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
  height: 450px; /* Chiều cao cố định để table có thể scroll */
}

.search-bar {
  flex-shrink: 0;
}

.table-wrapper {
  flex-grow: 1;
  overflow: auto; /* Cho phép table scroll nếu nội dung dài */
  border: 1px solid #e9eaeb;
  border-radius: 8px;
}

.custom-settings-table {
  width: 100%;
  border-collapse: collapse;
}

.custom-settings-table th {
  position: sticky;
  top: 0;
  background-color: #fafafa;
  z-index: var(--z-index-table-header);
  height: 36px;
  padding: 0 12px;
  text-align: left;
  font-weight: 600;
  font-size: 13px;
  border-bottom: 1px solid #e9eaeb;
}

.custom-settings-table td {
  height: 44px;
  padding: 0 12px;
  border-bottom: 1px solid #e9eaeb;
}

.drag-handle {
  cursor: move;
  color: #717680;
}

.actions-cell {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 8px;
  height: 100%;
}

.footer-actions {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

/* Style for the row being dragged */
.sortable-ghost {
  opacity: 0.5;
  background: #c8ebfb;
}
</style>
