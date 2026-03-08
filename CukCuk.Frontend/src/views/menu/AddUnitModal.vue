<template>
  <BaseModal
    :visible="visible"
    @update:visible="handleClose"
    info="Thêm nhanh đơn vị tính của món ăn"
  >
    <template #title>Thêm đơn vị tính</template>

    <FormInputSection>
      <FormInputRow label="Tên đơn vị" required :error="errors.unitName" label-width="150px">
        <Input
          v-model="unit.unitName"
          placeholder="Nhập tên đơn vị"
          :error="!!errors.unitName"
          @blur="validateField('unitName')"
        />
      </FormInputRow>
      <FormInputRow
        label="Diễn giải"
        :error="errors.unitDescription"
        align-items="flex-start"
        label-width="150px"
      >
        <textarea
          v-model="unit.unitDescription"
          class="textarea-custom"
          :class="{ 'input--error': errors.unitDescription }"
          placeholder="Nhập diễn giải"
          @blur="validateField('unitDescription')"
        ></textarea>
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
import unitService from '@/services/unit-service.js'
import Unit from '@/models/unit.js'

const props = defineProps({
  visible: Boolean,
})

const emit = defineEmits(['update:visible', 'save'])

const { showToast } = useToast()
const unit = ref(new Unit())
const errors = ref({})

const validationSchema = yup.object({
  unitName: yup
    .string()
    .required('Tên đơn vị không được để trống.')
    .max(100, 'Tên đơn vị không được quá 100 ký tự.'),
  unitDescription: yup.string().nullable().max(500, 'Diễn giải không được quá 500 ký tự.'),
})

// Reset form state
function resetForm() {
  unit.value = new Unit()
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
    await validationSchema.validateAt(field, unit.value)
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
    await validationSchema.validate(unit.value, { abortEarly: false })
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
    const result = await unitService.create(unit.value)
    showToast('Thêm đơn vị tính thành công!', 'success')
    emit('save', result) // Emit the newly created unit
    handleClose()
  } catch (error) {
    console.error('Failed to create unit:', error)
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
.textarea-custom {
  border: 1px solid #d0d5dd;
  border-radius: 8px;
  padding: 8px 12px;
  font-size: 13px;
  font-family: inherit;
  width: 100%;
  box-sizing: border-box;
  transition: border-color 0.2s;
  height: 100px;
  resize: vertical;
}

.textarea-custom:focus {
  outline: none;
  border-color: #1570ef;
}

.textarea-custom.input--error {
  border-color: #e61d1d;
}

.textarea-custom::placeholder {
  color: #9ca3af;
}

/* Nới rộng content để hiển thị bubble validation ở dòng cuối cùng mà không bị cuộn */
:deep(.form_content) {
  padding-bottom: 40px;
}
</style>
