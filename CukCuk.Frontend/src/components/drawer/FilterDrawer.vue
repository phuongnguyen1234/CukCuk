<template>
  <BaseDrawer title="Bộ lọc" @close="$emit('close')">
    <div class="filter-container">
      <Searchbar v-model="searchText" />

      <div class="filter-list">
        <div v-for="item in filteredColumns" :key="item.key" class="filter-item">
          <div class="filter-item-row">
            <input type="checkbox" :id="item.key" v-model="item.checked" class="m-checkbox" />
            <label :for="item.key" class="filter-label">{{ item.label }}</label>
          </div>

          <div v-if="item.checked" class="filter-details">
            <!-- Kiểu String -->
            <template v-if="item.type === 'text' || item.type === 'string'">
              <Select
                v-model="item.operator"
                :options="stringOperations"
                option-label="label"
                option-value="value"
              />
              <InputTag v-model="item.value" />
            </template>

            <!-- Kiểu Number -->
            <template v-else-if="item.type === 'number'">
              <Select
                v-model="item.operator"
                :options="numberOperations"
                option-label="label"
                option-value="value"
              />
              <Input v-model="item.value" type="number" placeholder="Nhập giá trị" />
            </template>

            <!-- Kiểu Boolean -->
            <template v-else-if="item.type === 'bool' || item.type === 'boolean'">
              <SelectMultipleTags
                v-model="item.value"
                :options="boolOptions"
                placeholder="Chọn trạng thái"
              />
            </template>

            <!-- Mặc định -->
            <Input v-else v-model="item.value" placeholder="Nhập giá trị lọc" />
          </div>
        </div>
      </div>
    </div>

    <template #footer>
      <ButtonGroup class="filter-footer-btn">
        <Button variant="outline" @click="resetFilters">Bỏ lọc</Button>
        <Button variant="primary" @click="applyFilters">Áp dụng</Button>
      </ButtonGroup>
    </template>
  </BaseDrawer>
</template>

<script setup>
import BaseDrawer from '@/components/base/BaseDrawer.vue'
import Input from '@/components/controls/inputs/Input.vue'
import Searchbar from '@/components/controls/inputs/Searchbar.vue'
import Button from '@/components/controls/buttons/Button.vue'
import ButtonGroup from '@/components/controls/buttons/ButtonGroup.vue'
import Select from '@/components/controls/selects/Select.vue'
import InputTag from '@/components/controls/inputs/InputTag.vue'
import SelectMultipleTags from '@/components/controls/selects/SelectMultipleTags.vue'
import { FilterOperation } from '@/models/enums/filter-operation.js'
import { ref, computed, watch } from 'vue'

const props = defineProps({
  columns: {
    type: Array,
    default: () => [],
  },
})

const emit = defineEmits(['close', 'apply'])

const searchText = ref('')
const filterState = ref([])

const stringOperations = [
  { value: FilterOperation.Contains, label: 'Chứa' },
  { value: FilterOperation.NotContains, label: 'Không chứa' },
  { value: FilterOperation.Equals, label: 'Bằng' },
  { value: FilterOperation.StartsWith, label: 'Bắt đầu bằng' },
  { value: FilterOperation.EndsWith, label: 'Kết thúc bằng' },
]

const numberOperations = [
  { value: FilterOperation.Equals, label: 'Bằng' },
  { value: FilterOperation.NotEqual, label: 'Không bằng' },
  { value: FilterOperation.GreaterThan, label: 'Lớn hơn' },
  { value: FilterOperation.LessThan, label: 'Nhỏ hơn' },
  { value: FilterOperation.GreaterThanOrEqual, label: 'Lớn hơn hoặc bằng' },
  { value: FilterOperation.LessThanOrEqual, label: 'Nhỏ hơn hoặc bằng' },
]

const boolOptions = ['Có', 'Không']

watch(
  () => props.columns,
  (newCols) => {
    // Tạo map từ state hiện tại để tra cứu nhanh
    const currentMap = new Map(filterState.value.map((i) => [i.key, i]))

    filterState.value = newCols.map((col) => {
      const existing = currentMap.get(col.key)
      // Nếu đã có state cũ thì giữ lại giá trị user đã nhập/chọn, chỉ cập nhật options mới
      if (existing) {
        return {
          ...col,
          checked: existing.checked,
          operator: existing.operator,
          value: existing.value,
        }
      }
      // Nếu chưa có thì khởi tạo mặc định
      return {
        ...col,
        checked: false,
        operator:
          col.type === 'number'
            ? FilterOperation.Equals
            : col.type === 'boolean' || col.type === 'bool'
              ? FilterOperation.Equals
              : FilterOperation.Contains,
        value: col.type === 'number' ? '' : [],
        options: col.options || [],
      }
    })
  },
  { immediate: true },
)

const filteredColumns = computed(() => {
  if (!searchText.value) return filterState.value
  const lower = searchText.value.toLowerCase()
  return filterState.value.filter((col) => col.label.toLowerCase().includes(lower))
})

function resetFilters() {
  filterState.value.forEach((item) => {
    item.checked = false
    item.value = item.type === 'number' ? '' : []
    item.operator =
      item.type === 'number'
        ? FilterOperation.Equals
        : item.type === 'boolean' || item.type === 'bool'
          ? FilterOperation.Equals
          : FilterOperation.Contains
  })
  emit('apply', [])
}

function applyFilters() {
  const activeFilters = filterState.value.filter((item) => item.checked)
  emit('apply', activeFilters)
}
</script>

<style scoped>
.filter-container {
  display: flex;
  flex-direction: column;
  gap: 12px;
  height: 100%;
}

.filter-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
  overflow-y: auto;
  flex: 1;
}

.filter-item {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.filter-item-row {
  display: flex;
  align-items: center;
  gap: 8px;
}

.filter-label {
  font-weight: 500;
  font-size: 14px;
  cursor: pointer;
}

.filter-details {
  display: flex;
  flex-direction: column;
  gap: 8px;
  padding-left: 24px;
}

.m-checkbox {
  width: 16px;
  height: 16px;
  cursor: pointer;
}

.filter-footer-btn {
  width: 100%;
  justify-content: space-between;
}
</style>
