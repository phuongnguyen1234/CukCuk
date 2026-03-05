<template>
  <BaseModal :visible="visible" @update:visible="handleClose">
    <template #title>Thêm nhóm thực đơn</template>

    <FormInputSection>
      <FormInputRow
        label="Mã nhóm"
        required
        :error="errors.inventoryItemCategoryCode"
        label-width="150px"
      >
        <Input
          v-model="category.inventoryItemCategoryCode"
          placeholder="Nhập mã nhóm"
          @blur="validateField('inventoryItemCategoryCode')"
        />
      </FormInputRow>
      <FormInputRow
        label="Tên nhóm"
        required
        :error="errors.inventoryItemCategoryName"
        label-width="150px"
      >
        <Input
          v-model="category.inventoryItemCategoryName"
          placeholder="Nhập tên nhóm"
          @blur="validateField('inventoryItemCategoryName')"
        />
      </FormInputRow>
    </FormInputSection>

    <template #footer>
      <ButtonGroup>
        <Button variant="secondary" @click="handleClose">Hủy</Button>
        <Button variant="primary" @click="handleSave">Lưu</Button>
      </ButtonGroup>
    </template>
  </BaseModal>
</template>

<script setup>
import { ref, watch } from 'vue'
import * as yup from 'yup'
import { useToast } from '@/utils/use-toast'

// Base Components
import BaseModal from '@/components/base/BaseModal.vue'
import FormInputSection from '@/components/form/FormInputSection.vue'
import FormInputRow from '@/components/form/FormInputRow.vue'

// Controls
import Input from '@/components/controls/inputs/Input.vue'
import Button from '@/components/controls/buttons/Button.vue'
import ButtonGroup from '@/components/controls/buttons/ButtonGroup.vue'

// Services and Models
import inventoryItemCategoryService from '@/services/inventory-item-category-service.js'
import InventoryItemCategory from '@/models/inventory-item-category.js'

const props = defineProps({
  visible: Boolean,
})

const emit = defineEmits(['update:visible', 'save'])

const { showToast } = useToast()
const category = ref(new InventoryItemCategory())
const errors = ref({})

const validationSchema = yup.object({
  inventoryItemCategoryName: yup
    .string()
    .required('Tên nhóm không được để trống.')
    .max(100, 'Tên nhóm không được quá 100 ký tự.'),
  inventoryItemCategoryCode: yup
    .string()
    .required('Mã nhóm không được để trống.')
    .max(255, 'Mã nhóm không được quá 255 ký tự.'),
})

// Reset form state
function resetForm() {
  category.value = new InventoryItemCategory()
  errors.value = {}
}

// Close modal
function handleClose() {
  emit('update:visible', false)
}

// Reset form when modal becomes visible
watch(
  () => props.visible,
  (isVisible) => {
    if (isVisible) {
      resetForm()
    }
  },
)

// Validate a single field
async function validateField(field) {
  try {
    await validationSchema.validateAt(field, category.value)
    if (errors.value[field]) {
      delete errors.value[field]
    }
  } catch (err) {
    errors.value[field] = err.message
  }
}

// Validate the whole form
async function validateForm() {
  try {
    errors.value = {}
    await validationSchema.validate(category.value, { abortEarly: false })
    return true
  } catch (err) {
    if (err instanceof yup.ValidationError) {
      err.inner.forEach((error) => {
        if (error.path) errors.value[error.path] = error.message
      })
    }
    return false
  }
}

// Save handler
async function handleSave() {
  const isValid = await validateForm()
  if (!isValid) {
    showToast('Dữ liệu không hợp lệ. Vui lòng kiểm tra lại.', 'error')
    return
  }

  try {
    const result = await inventoryItemCategoryService.create(category.value)
    showToast('Thêm nhóm thực đơn thành công!', 'success')
    emit('save', result) // Emit the newly created category
    handleClose()
  } catch (error) {
    console.error('Failed to create category:', error)
    showToast(error.response?.data?.UserMessage || 'Thêm mới thất bại.', 'error')
  }
}
</script>
