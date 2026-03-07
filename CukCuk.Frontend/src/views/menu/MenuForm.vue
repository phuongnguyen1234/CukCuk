<template>
  <ThePageContainer :title="title" :can-go-back="true" @back="handleCancel">
    <Loading v-if="isLoading" />
    <template #header-left>
      <div class="header-left-content">
        <SelectLabel
          v-model="item.inventoryItemTypeId"
          label="Loại món"
          :options="typeOptions"
          option-label="inventoryItemTypeName"
          option-value="inventoryItemTypeId"
          placeholder="Chọn loại món"
          :error="errors.inventoryItemTypeId"
          class="type-select"
        />
      </div>
    </template>

    <div class="menu-form-body">
      <Tabs v-model="activeTab" :tabs="tabs" class="menu-tabs">
        <template #general>
          <GeneralTab
            v-model="item"
            :category-options="categoryOptions"
            :unit-options="unitOptions"
            :kitchen-options="kitchenOptions"
            :errors="errors"
            @validate-field="validateField"
            @add-new-category="isAddCategoryModalVisible = true"
            @add-new-unit="isAddUnitModalVisible = true"
          />
        </template>
        <template #serving>
          <AdditionTab
            v-model="item"
            :addition-options="additionOptions"
            @add-new-addition="isAddAdditionModalVisible = true"
          />
        </template>
        <template #ingredients>
          <div class="tab-content-placeholder">Nội dung Định lượng nguyên vật liệu</div>
        </template>
        <template #price-policy>
          <div class="tab-content-placeholder">Nội dung Chính sách giá bán</div>
        </template>
        <template #footer>
          <div class="form-footer">
            <ButtonGroup>
              <Button variant="secondary" @click="discardChangesAndClose">Hủy</Button>
              <Button v-if="!isEditMode" variant="secondary" @click="onSaveAndAdd"
                >Lưu và Thêm</Button
              >
              <Button variant="primary" @click="onSave">Lưu</Button>
            </ButtonGroup>
          </div>
        </template>
      </Tabs>
    </div>

    <AddCategoryModal v-model:visible="isAddCategoryModalVisible" @save="handleCategorySaved" />
    <AddUnitModal v-model:visible="isAddUnitModalVisible" @save="handleUnitSaved" />
    <AddAdditionModal v-model:visible="isAddAdditionModalVisible" @save="handleAdditionSaved" />
    <ConfirmationModal
      v-model:visible="isCancelConfirmVisible"
      message="Dữ liệu đã thay đổi. Bạn có muốn cất không?"
      confirm-button-text="Cất"
      cancel-button-text="Không cất"
      confirm-button-variant="primary"
      :message="cancelConfirmConfig.message"
      :confirm-button-text="cancelConfirmConfig.confirmButtonText"
      :cancel-button-text="cancelConfirmConfig.cancelButtonText"
      @confirm="cancelConfirmConfig.confirmHandler"
      @cancel="cancelConfirmConfig.cancelHandler"
    />
  </ThePageContainer>
</template>

<script setup>
import { ref, watch, onMounted, computed } from 'vue'
import { useToast } from '@/utils/use-toast'
import ThePageContainer from '@/layouts/ThePageContainer.vue'
import Tabs from '@/components/Tabs.vue'
import GeneralTab from './GeneralTab.vue'
import ButtonGroup from '@/components/controls/buttons/ButtonGroup.vue'
import Button from '@/components/controls/buttons/Button.vue'
import SelectLabel from '@/components/controls/selects/SelectLabel.vue'
import ConfirmationModal from '@/components/base/ConfirmationModal.vue'
import * as yup from 'yup'
import AdditionTab from './AdditionTab.vue'
import InventoryItemService from '@/services/inventory-item-service.js'
import inventoryItemCategoryService from '@/services/inventory-item-category-service.js'
import unitService from '@/services/unit-service.js'
import kitchenService from '@/services/kitchen-service.js'
import inventoryItemAdditionService from '@/services/inventory-item-addition-service.js'
import inventoryItemTypeService from '@/services/inventory-item-type-service.js'

import AddCategoryModal from './AddCategoryModal.vue'
import AddUnitModal from './AddUnitModal.vue'
import AddAdditionModal from './AddAdditionModal.vue'
import Loading from '@/components/base/Loading.vue'

const props = defineProps({
  modelValue: {
    type: Object,
    default: () => ({}),
  },
  title: {
    type: String,
    default: 'Thêm món',
  },
})

const emit = defineEmits(['close', 'save', 'save-and-add', 'update:modelValue'])

const DRAFT_STORAGE_KEY = 'cukcuk_new_item_draft'

const item = ref({ ...props.modelValue })
const activeTab = ref('general')
const { showToast } = useToast()
const errors = ref({})
const initialItemState = ref(null) // For change detection on cancel
const isCancelConfirmVisible = ref(false)
const isAddCategoryModalVisible = ref(false)
const cancelConfirmConfig = ref({
  message: '',
  confirmButtonText: '',
  cancelButtonText: '',
  confirmHandler: () => {},
  cancelHandler: () => {},
})
const isAddUnitModalVisible = ref(false)
const isAddAdditionModalVisible = ref(false)
const isLoading = ref(false)

const isEditMode = computed(() => !!item.value?.inventoryItemId)

const tabs = [
  { label: 'Thông tin chung', value: 'general' },
  { label: 'Sở thích phục vụ', value: 'serving' },
  { label: 'Định lượng nguyên vật liệu', value: 'ingredients' },
  { label: 'Chính sách giá bán', value: 'price-policy' },
]

// Refs for shared dropdown options
const categoryOptions = ref([])
const unitOptions = ref([])
const kitchenOptions = ref([])
const additionOptions = ref([])
const typeOptions = ref([])

// Load all dependencies for dropdowns once when the form is mounted
async function loadAllDependencies() {
  try {
    const [categories, units, kitchens, additions, types] = await Promise.all([
      inventoryItemCategoryService.getAll(),
      unitService.getAll(),
      kitchenService.getAll(),
      inventoryItemAdditionService.getAll(),
      inventoryItemTypeService.getAll(),
    ])
    categoryOptions.value = categories
    unitOptions.value = units
    kitchenOptions.value = kitchens.map((k) => ({
      label: k.kitchenName,
      value: k.kitchenId,
    }))
    additionOptions.value = additions.map((item) => ({
      label: item.inventoryItemAdditionName,
      value: item.inventoryItemAdditionId,
      price: item.inventoryItemAdditionPrice,
    }))
    typeOptions.value = types
  } catch (error) {
    console.error('Failed to load form dependencies:', error)
  }
}

async function handleCategorySaved(newCategory) {
  await loadAllDependencies()
  item.value.inventoryItemCategoryId = newCategory.inventoryItemCategoryId
}

async function handleUnitSaved(newUnit) {
  await loadAllDependencies()
  item.value.unitId = newUnit.unitId
}

async function handleAdditionSaved(newAddition) {
  await loadAllDependencies()

  // Create a mutable copy of the additions array from the item ref.
  const additionsCopy = [...(item.value.additions || [])]

  // Find the index of the last row that is empty.
  // An empty row is one that has been added to the UI but not yet assigned a service.
  let lastEmptyRowIndex = -1
  for (let i = additionsCopy.length - 1; i >= 0; i--) {
    if (!additionsCopy[i].inventoryItemAdditionId) {
      lastEmptyRowIndex = i
      break
    }
  }

  if (lastEmptyRowIndex !== -1) {
    // If an empty row is found, update it with the new data.
    additionsCopy[lastEmptyRowIndex] = {
      inventoryItemAdditionId: newAddition.inventoryItemAdditionId,
      inventoryItemAdditionPrice: newAddition.inventoryItemAdditionPrice,
    }
  } else {
    // If no empty row is found (e.g., all rows are filled), add a new row.
    additionsCopy.push({
      inventoryItemAdditionId: newAddition.inventoryItemAdditionId,
      inventoryItemAdditionPrice: newAddition.inventoryItemAdditionPrice,
    })
  }

  // Update the main item ref. This will propagate down to AdditionTab.
  item.value.additions = additionsCopy
  // Also update the additionIds array for validation and submission.
  item.value.additionIds = additionsCopy.map((ad) => ad.inventoryItemAdditionId).filter(Boolean)
}

const loadedId = ref(null)

watch(
  () => props.modelValue?.inventoryItemId,
  (newId) => {
    // Nếu ID không đổi thì không load lại API (tránh việc con emit lên làm cha load lại)
    if (newId === loadedId.value) return

    loadedId.value = newId

    if (newId) {
      isLoading.value = true
      // Chế độ sửa: gọi API để lấy dữ liệu chi tiết
      InventoryItemService.getById(newId)
        .then((data) => {
          // Map the array of kitchen objects to an array of IDs for the v-model
          data.kitchenIds = data.kitchens?.map((k) => k.kitchenId) || []
          item.value = data
          initialItemState.value = JSON.parse(JSON.stringify(data))
        })
        .catch((err) => {
          // Nếu có lỗi, đóng form và báo lỗi
          console.error('Failed to fetch item details:', err)
          showToast('Không thể tải chi tiết món.', 'error')
          emit('close')
        })
        .finally(() => {
          isLoading.value = false
        })
    } else {
      // Chế độ thêm mới: kiểm tra và load bản nháp
      loadedId.value = null
      const draftJSON = localStorage.getItem(DRAFT_STORAGE_KEY)
      let dataToUse
      if (draftJSON && !props.modelValue?.inventoryItemName) {
        try {
          const draftData = JSON.parse(draftJSON)
          item.value = draftData
          showToast('Đã khôi phục bản nháp chưa lưu.', 'success')
        } catch (e) {
          console.error('Failed to parse draft from localStorage', e)
          item.value = { ...props.modelValue, kitchenIds: [] } // Fallback
        }
        dataToUse = item.value
      } else {
        // Không có bản nháp, tạo món mới
        const newItem = { ...props.modelValue } || {}
        newItem.kitchenIds = [] // Default to empty array for new items
        dataToUse = newItem
      }
      item.value = dataToUse
      initialItemState.value = JSON.parse(JSON.stringify(dataToUse))
    }
  },
  { immediate: true },
)

onMounted(loadAllDependencies)

// Helper function to prepare data for submission
function preparePayload() {
  const payload = { ...item.value }
  const imageFile = payload.imageFile

  // Remove properties that are for UI only or handled separately
  delete payload.imageFile
  delete payload.kitchens // Backend uses kitchenIds
  delete payload.additions // Backend uses additionIds

  return { payload, imageFile }
}

const validationSchema = yup.object({
  inventoryItemName: yup.string().trim().required('Tên món không được bỏ trống'),
  inventoryItemCode: yup
    .string()
    .trim()
    .required('Mã món không được bỏ trống')
    .max(255, 'Mã món không được vượt quá 255 ký tự'),
  unitId: yup.string().required('Đơn vị tính không được bỏ trống'),
  inventoryItemPrice: yup
    .number()
    .typeError('Giá bán phải là một con số')
    .required('Giá bán không được bỏ trống')
    .min(0, 'Giá bán không được là số âm')
    .integer('Giá bán phải là số nguyên'),
  inventoryItemTypeId: yup.string().required('Loại món không được bỏ trống'),
  inventoryItemDescription: yup.string().nullable().max(500, 'Mô tả không được vượt quá 500 ký tự'),
  inventoryItemLangName: yup
    .string()
    .nullable()
    .max(255, 'Tên món ngôn ngữ khác không được vượt quá 255 ký tự'),
  additionIds: yup
    .array()
    .test('is-unique', 'Mỗi sở thích phục vụ chỉ được chọn một lần.', (value) => {
      if (!value || value.length < 2) return true
      // Lọc ra các ID hợp lệ (khác rỗng) trước khi kiểm tra
      const validIds = value.filter((id) => id)
      // Kiểm tra xem số lượng ID duy nhất có bằng tổng số ID hợp lệ không
      return new Set(validIds).size === validIds.length
    }),
})

// Validate một trường cụ thể (dùng cho sự kiện blur)
async function validateField(field) {
  try {
    // validateAt chỉ kiểm tra trường được chỉ định dựa trên schema
    await validationSchema.validateAt(field, item.value)
    // Nếu hợp lệ và đang có lỗi thì xóa lỗi đi
    if (errors.value[field]) {
      delete errors.value[field]
    }
  } catch (err) {
    errors.value[field] = err.message
  }
}

async function validate() {
  try {
    errors.value = {}
    await validationSchema.validate(item.value, { abortEarly: false })
    return true
  } catch (err) {
    if (err instanceof yup.ValidationError) {
      const newErrors = {}
      err.inner.forEach((error) => {
        if (error.path) newErrors[error.path] = error.message
      })
      errors.value = newErrors

      // Tìm lỗi đầu tiên và chuyển đến tab tương ứng
      const firstErrorField = err.inner[0]?.path
      if (firstErrorField) {
        const generalTabFields = [
          'inventoryItemName',
          'inventoryItemCode',
          'unitId',
          'inventoryItemPrice',
          'inventoryItemTypeId',
          'inventoryItemDescription',
          'inventoryItemLangName',
        ]
        if (generalTabFields.includes(firstErrorField)) {
          activeTab.value = 'general'
        } else if (firstErrorField === 'additionIds') {
          activeTab.value = 'serving'
          // Vì không có trường input cụ thể cho lỗi này, hiển thị toast để thông báo
          showToast(newErrors.additionIds, 'error')
        }
        // Có thể thêm các 'else if' khác cho các tab trong tương lai
      }
    }
    return false
  }
}

// Watcher: Tự động xóa lỗi khi người dùng nhập liệu (Real-time validation)
watch(
  item,
  () => {
    // Duyệt qua các lỗi đang hiển thị, nếu trường đó đã hợp lệ thì xóa lỗi ngay
    Object.keys(errors.value).forEach((field) => {
      validateField(field)
    })
  },
  { deep: true },
)

function hasChanges() {
  if (!initialItemState.value) return false // Guard against null initial state

  // Create copies to avoid modifying original objects
  const current = { ...item.value }
  const initial = { ...initialItemState.value }

  // Check if a new image was added
  const imageAdded = !!item.value.imageFile

  // Remove properties that are not part of the data model or are handled separately
  delete current.imageFile
  delete initial.imageFile

  return JSON.stringify(current) !== JSON.stringify(initial) || imageAdded
}

function handleCancel() {
  if (hasChanges()) {
    if (isEditMode.value) {
      // Configure for EDIT mode
      cancelConfirmConfig.value = {
        message: 'Dữ liệu đã được thay đổi. Bạn có muốn lưu các thay đổi không?',
        confirmButtonText: 'Lưu',
        cancelButtonText: 'Không lưu',
        confirmHandler: onSave, // onSave will handle saving and the parent will close
        cancelHandler: discardChangesAndClose, // Just close without saving
      }
    } else {
      // Configure for ADD mode (saving a draft)
      cancelConfirmConfig.value = {
        message: 'Dữ liệu đã thay đổi. Bạn có muốn cất không?',
        confirmButtonText: 'Cất',
        cancelButtonText: 'Không cất',
        confirmHandler: saveDraftAndClose,
        cancelHandler: discardChangesAndClose,
      }
    }
    isCancelConfirmVisible.value = true // Show the modal
  } else {
    // No changes, just close
    discardChangesAndClose()
  }
}

function saveDraftAndClose() {
  const draftData = { ...item.value }
  delete draftData.imageFile // Don't save File object
  if (draftData.inventoryItemImage?.startsWith('blob:')) {
    draftData.inventoryItemImage = null // Don't save blob URLs
  }
  localStorage.setItem(DRAFT_STORAGE_KEY, JSON.stringify(draftData))
  showToast('Đã cất bản nháp thành công!')
  isCancelConfirmVisible.value = false
  emit('close')
}

function discardChangesAndClose() {
  isCancelConfirmVisible.value = false
  localStorage.removeItem(DRAFT_STORAGE_KEY)
  emit('close')
}

// Xử lý lỗi trả về từ backend
function handleApiError(error) {
  const responseData = error.response?.data

  // Case 1: Validation errors from backend (có object `errors`)
  if (responseData?.errors) {
    const backendErrors = responseData.errors
    const newErrors = {}
    let firstErrorMessage = ''

    // Lấy thông báo lỗi đầu tiên để hiển thị trong toast
    const errorValues = Object.values(backendErrors)
    if (errorValues.length > 0) {
      const firstError = errorValues[0]
      firstErrorMessage = Array.isArray(firstError) ? firstError[0] : firstError
    }

    // Xử lý tất cả lỗi để hiển thị bên cạnh các trường input
    for (const key in backendErrors) {
      // Chuyển PascalCase từ backend sang camelCase cho state ở frontend
      const camelCaseKey = key.charAt(0).toLowerCase() + key.slice(1)
      const errorValue = backendErrors[key]
      if (Array.isArray(errorValue)) {
        newErrors[camelCaseKey] = errorValue.join('; ')
      } else {
        newErrors[camelCaseKey] = errorValue // Nếu là string, gán trực tiếp
      }
    }
    errors.value = newErrors
    activeTab.value = 'general' // Chuyển đến tab có lỗi

    // Hiển thị thông báo lỗi đầu tiên, hoặc một thông báo chung nếu không có
    showToast(firstErrorMessage || 'Dữ liệu không hợp lệ, vui lòng kiểm tra lại.', 'error')
  }
  // Case 2: Các lỗi khác từ backend có message cụ thể
  else if (responseData?.message) {
    showToast(responseData.message, 'error')
  }
  // Case 3: Fallback cho các trường hợp lỗi khác
  else {
    showToast('Lưu thất bại. Có lỗi không xác định xảy ra.', 'error')
  }
}

async function onSave() {
  const isValid = await validate()
  if (!isValid) {
    showToast('Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các trường được đánh dấu.', 'error')
    return
  }
  try {
    if (isEditMode.value) {
      const { payload, imageFile } = preparePayload()
      const result = await InventoryItemService.update(payload.inventoryItemId, payload, imageFile)
      emit('save', result)
    } else {
      const { payload, imageFile } = preparePayload()
      const result = await InventoryItemService.create(payload, imageFile)
      localStorage.removeItem(DRAFT_STORAGE_KEY)
      emit('save', result)
    }
  } catch (error) {
    console.error('Failed to save item:', error)
    handleApiError(error)
  }
}

async function onSaveAndAdd() {
  if (isEditMode.value) return // Safety check, button is hidden in edit mode

  const isValid = await validate()
  if (!isValid) {
    showToast('Dữ liệu không hợp lệ. Vui lòng kiểm tra lại các trường được đánh dấu.', 'error')
    return
  }

  try {
    const { payload, imageFile } = preparePayload()
    await InventoryItemService.create(payload, imageFile)
    localStorage.removeItem(DRAFT_STORAGE_KEY)
    emit('save-and-add', payload)
  } catch (error) {
    console.error('Failed to save and add item:', error)
    handleApiError(error)
  }
}
</script>

<style scoped>
.header-left-content {
  display: flex;
  align-items: center;
  gap: 12px;
}

.header-label {
  font-weight: 500;
  font-size: 14px;
  color: #1f2937;
}

.type-select {
  width: 200px;
}

.menu-form-body {
  display: flex;
  flex-direction: column;
  height: 100%;
  overflow: hidden;
}

.menu-tabs {
  flex: 1;
  overflow-y: auto;
  min-height: 0;
}

.form-footer {
  width: calc(100% - 32px);
  max-width: 1094px;
  margin: 0 auto;
  display: flex;
  justify-content: flex-end;
  border-top: 1px solid #e9eaeb;
  padding: 12px 16px;
  gap: 16px;
  background-color: #fafafa;
  align-items: center;
  position: sticky;
  bottom: 0;
  z-index: 5;
}

.tab-content-placeholder {
  padding: 24px;
  text-align: center;
  color: #6b7280;
}
</style>
