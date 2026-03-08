<template>
  <div class="addition-tab-container">
    <FormSection title="Món ăn">
      <!-- Info Section -->
      <div class="info-box">
        <div class="info-icon">
          <svg
            width="16"
            height="16"
            viewBox="0 0 16 16"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M8 14.6667C11.6819 14.6667 14.6667 11.6819 14.6667 8C14.6667 4.3181 11.6819 1.33333 8 1.33333C4.3181 1.33333 1.33333 4.3181 1.33333 8C1.33333 11.6819 4.3181 14.6667 8 14.6667Z"
              stroke="#2E90FA"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M8 8V11.3333"
              stroke="#2E90FA"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M8 5.33333H8.00667"
              stroke="#2E90FA"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <span class="info-text"
          >Ghi lại các sở thích của khách hàng giúp nhân viên phục vụ chọn nhanh order.<br />
          VD: không cay/ ít hành/ thêm phomai...</span
        >
      </div>

      <!-- Action Bar & Table -->
      <div class="table-container">
        <!-- Action Bar -->
        <div class="content_body_actions">
          <div class="search_box">
            <Searchbar v-model="searchQuery" placeholder="Tìm kiếm" />
          </div>
        </div>

        <!-- Table -->
        <Table
          :headers="headers"
          :data="items"
          :show-vertical-borders="true"
          class="addition-table"
        >
          <!-- Cột Sở thích phục vụ -->
          <template #description="{ data, index }">
            <SelectTable
              v-model="data.description"
              :items="additionOptions"
              :columns="additionTableColumns"
              display-field="label"
              value-field="value"
              placeholder="Chọn sở thích phục vụ"
              dropdown-width="450px"
              :show-add-button="true"
              :searchable="true"
              @add="$emit('add-new-addition')"
              @change="(item) => onAdditionChange(item, index)"
            />
          </template>

          <!-- Cột Thu thêm -->
          <template #surcharge="{ data }">
            <span>{{ formatCurrency(data.surcharge || 0) }}</span>
          </template>

          <!-- Row Actions (Chỉ có nút xóa) -->
          <template #row-actions="{ index }">
            <ButtonIcon variant="danger-outline" title="Xóa" @click="removeRow(index)">
              <svg
                width="16"
                height="16"
                viewBox="0 0 16 16"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M2 4H3.33333H14M12.6667 4V13.3333C12.6667 13.687 12.5262 14.0261 12.2761 14.2761C12.0261 14.5262 11.687 14.6667 11.3333 14.6667H4.66667C4.31304 14.6667 3.97391 14.5262 3.72386 14.2761C3.47381 14.0261 3.33333 13.687 3.33333 13.3333V4M5.33333 4V2.66667C5.33333 2.31305 5.47381 1.97391 5.72386 1.72386C5.97391 1.47381 6.31304 1.33333 6.66667 1.33333H9.33333C9.68696 1.33333 10.0261 1.47381 10.2761 1.72386C10.5262 1.97391 10.6667 2.31305 10.6667 2.66667V4M6.66667 7.33333V11.3333M9.33333 7.33333V11.3333"
                  stroke="currentColor"
                  stroke-width="1.5"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                />
              </svg>
            </ButtonIcon>
          </template>
        </Table>

        <!-- Add Button -->
        <div class="add-btn-container">
          <Button variant="outline" @click="addRow">
            <template #icon>
              <i class="fi fi-rr-plus"></i>
            </template>
            Thêm dòng
          </Button>
        </div>
      </div>
    </FormSection>
  </div>
</template>

<script setup>
import { ref, watch, nextTick } from 'vue'
import FormSection from '@/components/form/FormSection.vue'
import Table from '@/components/controls/tables/Table.vue'
import Button from '@/components/controls/buttons/Button.vue'
import ButtonIcon from '@/components/controls/buttons/ButtonIcon.vue'
import Searchbar from '@/components/controls/inputs/Searchbar.vue'
import SelectTable from '@/components/controls/selects/SelectTable.vue'
import { useToast } from '@/utils/use-toast'
import { formatCurrency } from '@/utils/format-util.js'

const props = defineProps({
  modelValue: {
    type: Object,
    default: () => ({}),
  },
  additionOptions: {
    type: Array,
    default: () => [],
  },
})

const emit = defineEmits(['update:modelValue', 'add-new-addition'])

const { showToast } = useToast()
const searchQuery = ref('')
const items = ref([]) // Dữ liệu các dòng trong bảng

const headers = [
  { key: 'description', label: 'Sở thích phục vụ', type: 'custom', align: 'left', width: 'auto' },
  { key: 'surcharge', label: 'Thu thêm', type: 'custom', align: 'left', width: '200px' },
]

const additionTableColumns = [
  { key: 'label', label: 'Sở thích phục vụ', width: 'auto' },
  { key: 'price', label: 'Thu thêm', type: 'currency', align: 'right', width: '120px' },
]

// Watch dữ liệu từ API đổ vào (chỉ chạy khi additions thay đổi - tức là khi load form)
watch(
  () => props.modelValue.additions,
  (newAdditions) => {
    if (newAdditions && newAdditions.length > 0) {
      const newItems = newAdditions.map((ad) => ({
        description: ad.inventoryItemAdditionId, // ID của sở thích
        surcharge: ad.inventoryItemAdditionPrice, // Giá thu thêm
      }))

      // Chỉ cập nhật items nếu dữ liệu mới khác dữ liệu hiện tại
      // Điều này ngăn vòng lặp: items thay đổi -> emit additions -> props thay đổi -> watch chạy -> set items
      if (JSON.stringify(newItems) !== JSON.stringify(items.value)) {
        items.value = newItems
      }
    } else {
      // Nếu chưa có dữ liệu (món mới hoặc chưa load xong), giữ nguyên hoặc reset nếu cần
      if (items.value.length === 0) {
        items.value = [{ description: '', surcharge: 0 }]
      }
    }
  },
  { immediate: true },
)

// Watch sự thay đổi của bảng để cập nhật lại modelValue (gửi additionIds lên form cha)
watch(
  items,
  (newItems) => {
    // Lấy danh sách ID các sở thích đã chọn (bỏ qua các dòng chưa chọn)
    const ids = newItems.map((i) => i.description).filter((id) => id)

    // Tạo danh sách additions đầy đủ để lưu trạng thái hiển thị (cho trường hợp chuyển tab)
    const uiAdditions = newItems.map((i) => ({
      inventoryItemAdditionId: i.description,
      inventoryItemAdditionPrice: i.surcharge,
    }))

    emit('update:modelValue', {
      ...props.modelValue,
      additionIds: ids,
      additions: uiAdditions, // Cập nhật cả additions để giữ trạng thái UI
    })
  },
  { deep: true },
)

function addRow() {
  items.value.push({ description: '', surcharge: 0 })
}

function removeRow(index) {
  if (items.value.length <= 1) {
    // Nếu chỉ còn 1 dòng thì reset nó thay vì xóa
    items.value = [{ description: '', surcharge: 0 }]
    return
  }
  items.value.splice(index, 1)
}

function onAdditionChange(selectedItem, index) {
  if (selectedItem && selectedItem.value) {
    const selectedId = selectedItem.value

    // Kiểm tra xem sở thích đã được chọn ở dòng khác chưa
    const isDuplicate = items.value.some(
      (item, i) => item.description === selectedId && i !== index,
    )

    if (isDuplicate) {
      showToast('Sở thích phục vụ này đã được chọn.', 'warning')
      // Dùng nextTick để đảm bảo DOM đã cập nhật trước khi reset giá trị,
      // tránh trường hợp component SelectTable chưa kịp xử lý.
      nextTick(() => {
        items.value[index].description = ''
        items.value[index].surcharge = 0
      })
      return
    }

    const correspondingOption = props.additionOptions.find((opt) => opt.value === selectedId)
    if (correspondingOption) items.value[index].surcharge = correspondingOption.price
  } else {
    // Xử lý trường hợp người dùng xóa lựa chọn
    items.value[index].surcharge = 0
  }
}
</script>

<style scoped>
.addition-tab-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
  padding: 16px;
  width: 100%;
  max-width: 1126px;
  margin: 0 auto;
}

.info-box {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 16px;
}

.info-icon {
  margin-top: 2px; /* Căn chỉnh icon khớp với dòng text đầu tiên */
  flex-shrink: 0; /* Đảm bảo icon không bị co lại */
}

.info-text {
  font-size: 13px;
  color: #2e90fa;
  line-height: 20px; /* Tăng khoảng cách dòng để dễ đọc hơn trên màn hình nhỏ */
}

.table-container {
  border: 1px solid #e0e6ec;
  border-radius: 8px;
  overflow: hidden;
}

.content_body_actions {
  background-color: white;
  padding: 12px;
  display: flex;
  align-items: center;
  border-bottom: 1px solid #e0e6ec;
}

.search_box {
  width: 100%;
  max-width: 300px; /* Linh hoạt độ rộng, không cứng nhắc 300px */
}

.add-btn-container {
  padding: 12px;
  border-top: 1px solid #e0e6ec;
  background-color: #fff;
}

/* Ghi đè để table không bị giãn quá to trong form */
.addition-table :deep(.content_body_table_wrapper) {
  flex: 0 1 auto;
}
</style>
