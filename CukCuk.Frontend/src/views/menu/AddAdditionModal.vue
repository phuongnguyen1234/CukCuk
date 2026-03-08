<template>
  <BaseModal :visible="visible" @update:visible="handleClose">
    <template #title>Thêm sở thích phục vụ</template>

    <FormInputSection>
      <FormInputRow
        label="Tên sở thích phục vụ"
        required
        :error="errors.inventoryItemAdditionName"
        label-width="150px"
      >
        <Input
          v-model="addition.inventoryItemAdditionName"
          placeholder="Nhập tên sở thích phục vụ"
          :error="!!errors.inventoryItemAdditionName"
          @blur="validateField('inventoryItemAdditionName')"
        />
      </FormInputRow>
      <FormInputRow label="Thu thêm" :error="errors.inventoryItemAdditionPrice" label-width="150px">
        <Input
          v-model="addition.inventoryItemAdditionPrice"
          placeholder="0"
          :rules="['currency']"
          :error="!!errors.inventoryItemAdditionPrice"
          style="text-align: right"
          @blur="validateField('inventoryItemAdditionPrice')"
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
import inventoryItemAdditionService from '@/services/inventory-item-addition-service.js'
import InventoryItemAddition from '@/models/inventory-item-addition.js'

const props = defineProps({
  visible: Boolean,
})

const emit = defineEmits(['update:visible', 'save'])

const { showToast } = useToast()
const addition = ref(new InventoryItemAddition())
const errors = ref({})

const validationSchema = yup.object({
  inventoryItemAdditionName: yup
    .string()
    .required('Tên sở thích phục vụ không được để trống.')
    .max(100, 'Tên sở thích phục vụ không được quá 100 ký tự.'),
  inventoryItemAdditionPrice: yup
    .number()
    .typeError('Giá thu thêm phải là một con số.')
    .min(0, 'Giá thu thêm không được là số âm.')
    .integer('Giá thu thêm phải là số nguyên.')
    .default(0),
})

// Reset form state
function resetForm() {
  addition.value = new InventoryItemAddition()
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
    await validationSchema.validateAt(field, addition.value)
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
    await validationSchema.validate(addition.value, { abortEarly: false })
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
    const result = await inventoryItemAdditionService.create(addition.value)
    showToast('Thêm sở thích phục vụ thành công!', 'success')
    emit('save', result) // Emit the newly created addition
    handleClose()
  } catch (error) {
    console.error('Failed to create addition:', error)
    const responseData = error.response?.data
    if (responseData?.errors) {
      const firstError = Object.values(responseData.errors)[0]
      const msg = Array.isArray(firstError) ? firstError[0] : firstError
      showToast(msg, 'error')
    } else {
      showToast(responseData?.Message || 'Thêm mới thất bại.', 'error')
    }
  }
}
</script>

<style scoped>
/* Nới rộng content để hiển thị bubble validation ở dòng cuối cùng mà không bị cuộn */
:deep(.form_content) {
  padding-bottom: 40px;
}
</style>
