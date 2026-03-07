<template>
  <!--content header-->
  <Loading v-if="isLoading" />

  <TheContent v-if="!isOpen" title="Thực đơn">
    <template #header>
      <ButtonGroup>
        <!--nhập từ excel-->
        <Button variant="outline">
          <template #icon>
            <svg
              width="17"
              height="20"
              viewBox="0 0 17 20"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M10 1V5C10 5.265 10.105 5.52 10.293 5.707C10.48 5.895 10.735 6 11 6H15M10 1H3C2.47 1 1.961 1.211 1.586 1.586C1.211 1.961 1 2.47 1 3V17C1 17.53 1.211 18.039 1.586 18.414C1.961 18.789 2.47 19 3 19H13C14.105 19 15 18.105 15 17V16M10 1L15 6M15 6C15 6 15 6.828 15 8M16 12H8M8 12L11 9M8 12L11 15"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              />
            </svg>
          </template>
          Nhập từ Excel</Button
        >

        <!--thêm mới-->
        <Button variant="primary" @click="openModal">
          <template #icon>
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M7.99992 3.33334V12.6667M3.33325 8H12.6666"
                stroke="white"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
              />
            </svg>
          </template>
          Thêm</Button
        >

        <!--menu 3 chấm-->
        <ButtonIcon variant="outline">
          <svg
            width="18"
            height="4"
            viewBox="0 0 18 4"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M1 2C1 2.265 1.11004 2.52 1.29004 2.707C1.48004 2.895 1.73 3 2 3C2.27 3 2.51996 2.895 2.70996 2.707C2.88996 2.52 3 2.265 3 2C3 1.735 2.88996 1.48 2.70996 1.293C2.51996 1.105 2.27 1 2 1C1.73 1 1.48004 1.105 1.29004 1.293C1.11004 1.48 1 1.735 1 2Z"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M8 2C8 2.265 8.11004 2.52 8.29004 2.707C8.48004 2.895 8.73 3 9 3C9.27 3 9.51996 2.895 9.70996 2.707C9.88996 2.52 10 2.265 10 2C10 1.735 9.88996 1.48 9.70996 1.293C9.51996 1.105 9.27 1 9 1C8.73 1 8.48004 1.105 8.29004 1.293C8.11004 1.48 8 1.735 8 2Z"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M15 2C15 2.265 15.11 2.52 15.29 2.707C15.48 2.895 15.73 3 16 3C16.27 3 16.52 2.895 16.71 2.707C16.89 2.52 17 2.265 17 2C17 1.735 16.89 1.48 16.71 1.293C16.52 1.105 16.27 1 16 1C15.73 1 15.48 1.105 15.29 1.293C15.11 1.48 15 1.735 15 2Z"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </ButtonIcon>
      </ButtonGroup>
    </template>

    <!--action bar-->
    <div class="content_body_actions">
      <!--search box-->
      <div class="search_box">
        <Searchbar v-model="searchQuery" />
      </div>

      <!--action btns-->
      <ButtonGroup>
        <!--btn refresh-->
        <ButtonIcon variant="outline" title="Làm mới" @click="loadData">
          <svg
            width="13"
            height="13"
            viewBox="0 0 13 13"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M11.3714 6.77904C11.2455 7.73901 10.8607 8.64661 10.2582 9.40449C9.65567 10.1624 8.85822 10.7419 7.95136 11.0811C7.04451 11.4202 6.06244 11.506 5.11049 11.3294C4.15855 11.1528 3.27263 10.7204 2.54776 10.0786C1.82288 9.43675 1.2864 8.6097 0.995838 7.68614C0.705277 6.76258 0.671602 5.77734 0.898426 4.8361C1.12525 3.89485 1.60401 3.03311 2.28337 2.34328C2.96273 1.65345 3.81706 1.16157 4.75472 0.920375C7.35406 0.253708 10.0447 1.59171 11.0381 4.08504M11.4161 0.751699V4.08503H8.08272"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </ButtonIcon>

        <!--btn export-->
        <ButtonIcon variant="outline" title="Xuất dữ liệu">
          <svg
            width="13"
            height="14"
            viewBox="0 0 13 14"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M6.75 0.75V3.41667C6.75 3.59348 6.82024 3.76305 6.94526 3.88807C7.07029 4.0131 7.23986 4.08333 7.41667 4.08333H10.0833M6.75 0.75H2.08333C1.72971 0.75 1.39057 0.890476 1.14052 1.14052C0.890476 1.39057 0.75 1.72971 0.75 2.08333V11.4167C0.75 11.7703 0.890476 12.1094 1.14052 12.3595C1.39057 12.6095 1.72971 12.75 2.08333 12.75H7.08333M6.75 0.75L10.0833 4.08333M10.0833 4.08333V5.08333M6.75 9.41667H12.0833M12.0833 9.41667L10.0833 7.41667M12.0833 9.41667L10.0833 11.4167"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </ButtonIcon>

        <!--btn setting-->
        <ButtonIcon variant="outline" title="Thiết lập" @click="isTableSettingVisible = true">
          <Setting color="currentColor" />
        </ButtonIcon>

        <!--btn filter-->
        <ButtonIcon
          variant="outline"
          title="Lọc"
          :active="isFilterDrawerOpen"
          @click="isFilterDrawerOpen = !isFilterDrawerOpen"
        >
          <!-- Icon mặc định (outline) -->
          <svg
            width="13"
            height="13"
            viewBox="0 0 13 13"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M0.75 0.75H11.4167V2.198C11.4166 2.55159 11.2761 2.89068 11.026 3.14067L8.08333 6.08333V10.75L4.08333 12.0833V6.41667L1.09667 3.13133C0.873632 2.88595 0.750035 2.56626 0.75 2.23467V0.75Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
          <!-- Icon khi active (filled) -->
          <template #icon-active>
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
          </template>
        </ButtonIcon>
      </ButtonGroup>
    </div>

    <!--content table-->
    <Table
      :headers="visibleTableHeaders"
      :data="tableData"
      :active-filter-keys="activeFilterKeys"
      @row-dblclick="editItem"
      @header-click="handleHeaderClick"
    >
      <!-- Row Actions Overlay -->
      <template #row-actions="{ data }">
        <ButtonGroup>
          <ButtonIcon variant="outline" title="Sửa" @click="editItem(data)">
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M12.6667 2.66667C13.0203 2.31305 13.5 2.11437 14 2.11437C14.5 2.11437 14.9797 2.31305 15.3333 2.66667C15.687 3.02029 15.8856 3.5 15.8856 4C15.8856 4.5 15.687 4.97971 15.3333 5.33333L5.33333 15.3333H2.66667V12.6667L12.6667 2.66667Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
              />
            </svg>
          </ButtonIcon>
          <ButtonIcon variant="outline" title="Nhân bản" @click="duplicateItem(data)">
            <svg
              width="16"
              height="16"
              viewBox="0 0 16 16"
              fill="none"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                d="M13.3333 6H14C14.3536 6 14.6928 6.14048 14.9428 6.39052C15.1929 6.64057 15.3333 6.97971 15.3333 7.33333V14C15.3333 14.3536 15.1929 14.6928 14.9428 14.9428C14.6928 15.1929 14.3536 15.3333 14 15.3333H7.33333C6.97971 15.3333 6.64057 15.1929 6.39052 14.9428C6.14048 14.6928 6 14.3536 6 14V13.3333M10.6667 10H4C3.64638 10 3.30724 9.85952 3.05719 9.60948C2.80714 9.35943 2.66667 9.02029 2.66667 8.66667V2C2.66667 1.64638 2.80714 1.30724 3.05719 1.05719C3.30724 0.807142 3.64638 0.666667 4 0.666667H10.6667C11.0203 0.666667 11.3594 0.807142 11.6095 1.05719C11.8595 1.30724 12 1.64638 12 2V8.66667C12 9.02029 11.8595 9.35943 11.6095 9.60948C11.3594 9.85952 11.0203 10 10.6667 10Z"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
              />
            </svg>
          </ButtonIcon>
          <ButtonIcon variant="danger-outline" title="Xóa" @click="deleteItem(data)">
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
        </ButtonGroup>
      </template>
    </Table>

    <!--pagination-->
    <Pagination
      :total-records="totalRecords"
      v-model:page-size="pageSize"
      v-model:current-page="currentPage"
    />

    <template #sidebar>
      <FilterDrawer
        v-show="isFilterDrawerOpen"
        :applied-filters="appliedFilters"
        :columns="filterDrawerColumns"
        @close="isFilterDrawerOpen = false"
        @apply="handleFilterApply"
      />
    </template>
  </TheContent>

  <MenuForm
    v-else
    :model-value="selectedItem"
    :title="formTitle"
    @close="isOpen = false"
    @save="handleSave"
    @save-and-add="handleSaveAndAdd"
  />

  <QuickFilterModal
    v-if="isQuickFilterVisible"
    v-model:visible="isQuickFilterVisible"
    :column="currentQuickFilterColumn"
    :filter="currentQuickFilterData"
    :top="quickFilterPosition.top"
    :left="quickFilterPosition.left"
    @apply="handleQuickFilterApply"
    @reset="handleQuickFilterReset"
  />

  <ConfirmationModal
    v-model:visible="isConfirmModalVisible"
    :message="confirmMessage"
    confirm-button-text="Xóa"
    confirm-button-variant="danger"
    @confirm="handleConfirmDelete"
  />

  <TableSetting
    v-if="isTableSettingVisible"
    :visible="isTableSettingVisible"
    :columns="columnsConfig"
    @save="handleColumnSettingsSave"
    @reset="handleColumnSettingsReset"
    @update:visible="isTableSettingVisible = $event"
  />
</template>

<style scoped>
/* style content */
.content_body_actions {
  background-color: white;
  border-radius: 5px 5px 0 0;
  padding: 12px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.search_box {
  width: 240px;
}

/*end of style content*/
</style>

<script setup>
import Table from '@/components/controls/tables/Table.vue'
import TheContent from '@/layouts/TheContent.vue'
import { ref, computed, onMounted, watch, reactive } from 'vue'
import { useToast } from '@/utils/use-toast'
import Button from '@/components/controls/buttons/Button.vue'
import ButtonGroup from '@/components/controls/buttons/ButtonGroup.vue'
import ButtonIcon from '@/components/controls/buttons/ButtonIcon.vue'
import Searchbar from '@/components/controls/inputs/Searchbar.vue'
import Setting from '@/components/icons/SettingIcon.vue'
import Pagination from '@/components/controls/pagination/Pagination.vue'
import { CourseText } from '@/models/enums/course.js'
import FilterDrawer from '@/components/drawer/FilterDrawer.vue'
import QuickFilterModal from '@/components/controls/tables/QuickFilterModal.vue'
import ConfirmationModal from '@/components/base/ConfirmationModal.vue'
import MenuForm from './MenuForm.vue'
import TableSetting from '@/components/base/TableSetting.vue'
import InventoryItemService from '@/services/inventory-item-service.js'
import Loading from '@/components/base/Loading.vue'
import { pascalToSnakeCase } from '@/utils/string-util.js'

const isOpen = ref(false)
const COLUMNS_CONFIG_STORAGE_KEY = 'cukcuk_menu_columns_config'
const items = ref([])
const isTableSettingVisible = ref(false)
const totalRecords = ref(0)
const searchQuery = ref('')
const debouncedSearchQuery = ref('')
const currentPage = ref(1)
const pageSize = ref(10)
const isFilterDrawerOpen = ref(false)
const appliedFilters = ref([])
const isQuickFilterVisible = ref(false)
const quickFilterPosition = ref({ top: '0px', left: '0px' })
const currentQuickFilterColumn = ref(null)
const isConfirmModalVisible = ref(false)
const itemToDelete = ref(null)
const confirmMessage = ref('')
const isLoading = ref(false)

const getDefaultColumns = () => [
  {
    key: 'inventoryItemTypeName',
    label: 'Loại món',
    type: 'text',
    align: 'left',
    width: 120,
    visible: true,
    pinned: false,
    filterable: true,
  },
  {
    key: 'inventoryItemCode',
    label: 'Mã món',
    type: 'text',
    align: 'left',
    width: 100,
    visible: true,
    pinned: false,
    filterable: true,
  },
  {
    key: 'inventoryItemName',
    label: 'Tên món',
    type: 'text',
    align: 'left',
    width: 200,
    visible: true,
    pinned: false,
    filterable: true,
  },
  {
    key: 'inventoryItemCategoryName',
    label: 'Nhóm thực đơn',
    type: 'text',
    align: 'left',
    width: 150,
    visible: true,
    pinned: false,
    filterable: true,
  },
  {
    key: 'unitName',
    label: 'Đơn vị tính',
    type: 'text',
    align: 'left',
    width: 100,
    visible: true,
    pinned: false,
    filterable: false,
  },
  {
    key: 'inventoryItemCostPrice',
    label: 'Giá vốn',
    type: 'currency',
    align: 'right',
    width: 120,
    visible: true,
    pinned: false,
    filterable: false,
  },
  {
    key: 'inventoryItemPrice',
    label: 'Giá bán',
    type: 'currency',
    align: 'right',
    width: 120,
    visible: true,
    pinned: false,
    filterable: false,
  },
  {
    key: 'inventoryItemIsMarketPrice',
    label: 'Thay đổi theo thời giá',
    type: 'boolean',
    align: 'center',
    width: 170,
    visible: true,
    pinned: false,
    filterable: false,
  },
  {
    key: 'inventoryItemAllowPriceOverride',
    label: 'Điều chỉnh giá tự do',
    type: 'boolean',
    align: 'center',
    width: 150,
    visible: true,
    pinned: false,
    filterable: false,
  },
  {
    key: 'inventoryItemIsFeatured',
    label: 'Món đặc trưng',
    type: 'boolean',
    align: 'center',
    width: 120,
    visible: true,
    pinned: false,
    filterable: false,
  },
  {
    key: 'inventoryItemIsOnSale',
    label: 'Đang bán',
    type: 'boolean',
    align: 'center',
    width: 100,
    visible: true,
    pinned: false,
    filterable: false,
  },
]

const loadColumnsConfig = () => {
  const defaultConfig = getDefaultColumns()
  const savedJSON = localStorage.getItem(COLUMNS_CONFIG_STORAGE_KEY)

  if (savedJSON) {
    try {
      const savedConfig = JSON.parse(savedJSON)
      const savedConfigMap = new Map(savedConfig.map((c) => [c.key, c]))

      // Lặp qua cấu hình mặc định để giữ nguyên cấu trúc và thứ tự
      const finalConfig = defaultConfig.map((defaultCol) => {
        const savedCol = savedConfigMap.get(defaultCol.key)
        // Nếu có cột đã lưu, hợp nhất các thuộc tính đã lưu vào cột mặc định
        return savedCol ? { ...defaultCol, ...savedCol } : defaultCol
      })
      return finalConfig
    } catch (e) {
      console.error('Không thể phân tích cài đặt cột, sử dụng cài đặt mặc định.', e)
      localStorage.removeItem(COLUMNS_CONFIG_STORAGE_KEY)
    }
  }

  return defaultConfig
}

const columnsConfig = ref(loadColumnsConfig())

// Lấy các cột sẽ hiển thị trong bảng
const visibleTableHeaders = computed(() => {
  return columnsConfig.value
    .filter((c) => c.visible)
    .sort((a, b) => {
      const aPinned = a.pinned === 'left'
      const bPinned = b.pinned === 'left'
      if (aPinned === bPinned) return 0
      return aPinned ? -1 : 1
    })
    .map((col) => {
      const normalizeSize = (value) => {
        if (!value) return undefined

        if (typeof value === 'number' || (typeof value === 'string' && /^\d+$/.test(value))) {
          return `${value}px`
        }

        return value
      }

      return {
        ...col,
        width: normalizeSize(col.width),
        minWidth: normalizeSize(col.minWidth),
        maxWidth: normalizeSize(col.maxWidth),
      }
    })
})

function handleColumnSettingsSave(newConfig) {
  columnsConfig.value = newConfig
  localStorage.setItem(COLUMNS_CONFIG_STORAGE_KEY, JSON.stringify(newConfig))
  isTableSettingVisible.value = false
  showToast('Đã cập nhật thiết lập bảng thành công!')
}

function handleColumnSettingsReset() {
  columnsConfig.value = getDefaultColumns()
  localStorage.removeItem(COLUMNS_CONFIG_STORAGE_KEY)
  showToast('Đã khôi phục thiết lập bảng mặc định!')
  isTableSettingVisible.value = false
}

// Tính toán columns kèm theo options cho SelectMultipleTags
const filterDrawerColumns = computed(() => {
  // Dùng columnsConfig làm nguồn để đảm bảo tất cả các cột đều có thể lọc,
  // ngay cả khi chúng đang bị ẩn
  return columnsConfig.value.map((col) => {
    // Nếu là cột text, tự động lấy danh sách các giá trị duy nhất từ items để làm options
    if (col.type === 'text') {
      const uniqueValues = [...new Set(items.value.map((item) => item[col.key]).filter(Boolean))]
      return { ...col, options: uniqueValues }
    }
    return col
  })
})

const selectedItem = ref(null)
const { showToast } = useToast()

const formTitle = computed(() =>
  selectedItem.value?.inventoryItemId ? 'Sửa món ăn' : 'Thêm món ăn',
)

function openModal() {
  isOpen.value = true
  selectedItem.value = {}
}

async function loadData() {
  isLoading.value = true
  try {
    // Xây dựng filter
    const filters = []

    if (appliedFilters.value.length > 0) {
      appliedFilters.value.forEach((f) => {
        // Chuyển đổi key từ camelCase sang snake_case để khớp với tên cột trong DB
        // Ví dụ: inventoryItemName -> inventory_item_name
        // Sử dụng f.key (theo baseColumns) hoặc f.column để đảm bảo lấy được tên cột
        const colKey = f.key || f.column
        if (colKey) {
          const column = pascalToSnakeCase(colKey)
          filters.push(`${column}:${f.operator}:${f.value}`)
        }
      })
    }

    // Truyền debouncedSearchQuery vào tham số thứ 4 (search)
    const response = await InventoryItemService.getAll(
      currentPage.value,
      pageSize.value,
      filters,
      debouncedSearchQuery.value,
    )

    items.value = response.items || []
    totalRecords.value = response.totalItems || 0
  } catch (error) {
    console.error('Failed to load data:', error)
    showToast('Không thể tải dữ liệu', 'error')
  } finally {
    isLoading.value = false
  }
}

let searchTimeout = null
watch(searchQuery, (newVal) => {
  if (searchTimeout) clearTimeout(searchTimeout)
  searchTimeout = setTimeout(() => {
    debouncedSearchQuery.value = newVal
  }, 300)
})

// Khi search hoặc pageSize thay đổi -> Reset về trang 1
watch([debouncedSearchQuery, pageSize], () => {
  currentPage.value = 1
  loadData()
})

// Khi currentPage thay đổi -> Load lại data
watch(currentPage, () => {
  loadData()
})

const tableData = computed(() => {
  // items bây giờ đã là dữ liệu của trang hiện tại từ API
  return items.value.map((item) => ({
    ...item,
    inventoryItemCourse: CourseText[item.inventoryItemCourse] || '',
  }))
})

const activeFilterKeys = computed(() => {
  return appliedFilters.value.map((f) => f.key)
})

const currentQuickFilterData = computed(() => {
  return appliedFilters.value.find((f) => f.key === currentQuickFilterColumn.value?.key)
})

function editItem(itemFromRow) {
  // Tìm item gốc, chưa qua định dạng trong mảng `items` để lấy dữ liệu thô
  const originalItem = items.value.find((i) => i.inventoryItemId === itemFromRow.inventoryItemId)

  // Sử dụng item gốc nếu tìm thấy, nếu không thì fallback về item từ row (có thể đã bị định dạng)
  selectedItem.value = originalItem ? { ...originalItem } : { ...itemFromRow }
  isOpen.value = true
}

function duplicateItem(itemFromRow) {
  // Tìm item gốc để đảm bảo nhân bản từ dữ liệu thô, chính xác
  const originalItem = items.value.find((i) => i.inventoryItemId === itemFromRow.inventoryItemId)

  if (originalItem) {
    const newItem = { ...originalItem }
    newItem.inventoryItemId = null // Reset ID để tạo mới
    newItem.inventoryItemCode = '' // Reset mã để cho phép sinh mã mới

    selectedItem.value = newItem
    isOpen.value = true
  }
}

async function handleSave(updatedItem) {
  // Logic lưu (Create/Update) nên được xử lý bên trong MenuForm hoặc gọi API tại đây
  // Sau khi lưu thành công, reload lại danh sách
  await loadData()
  isOpen.value = false
  showToast('Thêm món thành công!')
}

async function handleSaveAndAdd(itemJustSaved) {
  await loadData()

  // Chuẩn bị một món mới dựa trên món vừa lưu để người dùng tiếp tục thêm
  const newItem = { ...itemJustSaved }
  // Reset ID và Code cho lần nhập mới
  delete newItem.inventoryItemId
  newItem.inventoryItemCode = '' // Sẽ kích hoạt sinh mã tự động trong form

  selectedItem.value = newItem
  showToast('Thêm món thành công! Sẵn sàng thêm món mới.')
}

function deleteItem(item) {
  itemToDelete.value = item
  confirmMessage.value = `Bạn có chắc chắn muốn xóa món <${item.inventoryItemName}> không?`
  isConfirmModalVisible.value = true
}

async function handleConfirmDelete() {
  if (!itemToDelete.value) return

  try {
    await InventoryItemService.delete(itemToDelete.value.inventoryItemId)
    showToast('Xóa món thành công!')
    await loadData()
  } catch (error) {
    console.error(error)
    showToast('Xóa thất bại (Có thể món đã bị xóa rồi?)', 'error')
  } finally {
    isConfirmModalVisible.value = false
    itemToDelete.value = null
  }
}

function handleFilterApply(filters) {
  // console.log('Filters received:', filters) // Bỏ comment để debug nếu cần
  appliedFilters.value = filters
  currentPage.value = 1
  loadData()
}

function handleHeaderClick(header, event) {
  const targetRect = event.currentTarget.getBoundingClientRect()
  currentQuickFilterColumn.value = header
  quickFilterPosition.value = {
    top: `${targetRect.bottom}px`,
    left: `${targetRect.left}px`,
  }
  isQuickFilterVisible.value = true
}

function handleQuickFilterApply(newFilter) {
  const index = appliedFilters.value.findIndex((f) => f.key === newFilter.key)
  if (index > -1) {
    // Cập nhật bộ lọc hiện có
    appliedFilters.value[index] = newFilter
  } else {
    // Thêm bộ lọc mới
    appliedFilters.value.push(newFilter)
  }
  loadData()
}

function handleQuickFilterReset(filterKey) {
  appliedFilters.value = appliedFilters.value.filter((f) => f.key !== filterKey)
  loadData()
}

onMounted(() => {
  loadData()
})
</script>
