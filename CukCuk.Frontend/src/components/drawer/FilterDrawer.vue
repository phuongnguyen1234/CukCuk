<template>
  <BaseDrawer title="Bộ lọc" @close="$emit('close')">
    <div class="filter-container">
      <Searchbar v-model="searchText" />

      <div class="filter-list">
        <div
          v-for="item in filteredColumns"
          :key="item.key"
          class="filter-item"
          :class="{ 'is-active': item.checked }"
        >
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
            <template v-else-if="item.type === 'number' || item.type === 'currency'">
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
  appliedFilters: {
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
  () => [props.columns, props.appliedFilters],
  ([newCols, newAppliedFilters]) => {
    const appliedMap = new Map(newAppliedFilters.map((f) => [f.key, f]))

    filterState.value = newCols.map((col) => {
      const applied = appliedMap.get(col.key)
      // Nếu cột này đang được áp dụng bộ lọc từ bên ngoài, khởi tạo state với giá trị đó
      if (applied) {
        return {
          ...col,
          checked: true,
          operator: applied.operator,
          value: applied.value,
          options: col.options || [],
        }
      }
      // Nếu không, khởi tạo state mặc định
      return {
        ...col,
        checked: false,
        operator:
          col.type === 'number' || col.type === 'currency'
            ? FilterOperation.Equals
            : col.type === 'boolean' || col.type === 'bool'
              ? FilterOperation.Equals
              : FilterOperation.Contains,
        value: col.type === 'number' || col.type === 'currency' ? '' : [],
        options: col.options || [],
      }
    })
  },
  { immediate: true, deep: true },
)

const filteredColumns = computed(() => {
  if (!searchText.value) return filterState.value
  const lower = searchText.value.toLowerCase()
  return filterState.value.filter((col) => col.label.toLowerCase().includes(lower))
})

function resetFilters() {
  filterState.value.forEach((item) => {
    item.checked = false
    item.value = item.type === 'number' || item.type === 'currency' ? '' : []
    item.operator =
      item.type === 'number' || item.type === 'currency'
        ? FilterOperation.Equals
        : item.type === 'boolean' || item.type === 'bool'
          ? FilterOperation.Equals
          : FilterOperation.Contains
  })
  // Sau khi bỏ lọc, áp dụng ngay thay đổi (danh sách rỗng)
  applyFilters()
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
  padding: 0 8px;
}

.filter-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.filter-item {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.filter-item.is-active {
  background-color: #f0f6fe;
  border-radius: 8px;
  padding: 8px;
  margin: 0 -8px;
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
