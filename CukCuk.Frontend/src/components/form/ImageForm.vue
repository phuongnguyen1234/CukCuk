<template>
  <div class="image-form">
    <!-- Input file ẩn -->
    <input
      ref="fileInput"
      type="file"
      style="display: none"
      accept=".jpg, .jpeg, .png, .gif"
      @change="onFileSelected"
    />
    <!-- 1. Ảnh -->
    <div class="image-preview">
      <img v-if="imageUrl" :src="imageUrl" alt="Ảnh món" class="preview-img" />
      <div v-else class="image-placeholder">
        <img :src="ImgPlaceholder" alt="placeholder" height="90" width="79" />
      </div>
    </div>

    <!-- 2. Text Ảnh món -->
    <div class="image-label">Ảnh món</div>

    <!-- 3. Text hướng dẫn -->
    <div class="image-help">
      Chọn các ảnh có định dạng<br />
      (.jpg, .jpeg, .png, .gif)
    </div>

    <!-- 4. Actions -->
    <ButtonGroup class="image-actions">
      <Button
        variant="outline"
        class="btn-upload"
        :text-size="12"
        :height="28"
        :width="86"
        :padding="'4px 12px'"
        @click="triggerUpload"
      >
        <template #icon>
          <svg
            width="16"
            height="16"
            viewBox="0 0 16 16"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M2.6665 11.3333V12.6667C2.6665 13.0203 2.80698 13.3594 3.05703 13.6095C3.30708 13.8595 3.64622 14 3.99984 14H11.9998C12.3535 14 12.6926 13.8595 12.9426 13.6095C13.1927 13.3594 13.3332 13.0203 13.3332 12.6667V11.3333M4.6665 5.99999L7.99984 2.66666M7.99984 2.66666L11.3332 5.99999M7.99984 2.66666V10.6667"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </template>
        Tải lên
      </Button>

      <ButtonIcon
        variant="outline"
        title="Tùy chọn"
        :height="28"
        :width="28"
        :min-width="28"
        @click="$emit('menu', $event)"
      >
        <svg
          width="16"
          height="4"
          viewBox="0 0 16 4"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M2 0C0.9 0 0 0.9 0 2C0 3.1 0.9 4 2 4C3.1 4 4 3.1 4 2C4 0.9 3.1 0 2 0ZM14 0C12.9 0 12 0.9 12 2C12 3.1 12.9 4 14 4C15.1 4 16 3.1 16 2C16 0.9 15.1 0 14 0ZM8 0C6.9 0 6 0.9 6 2C6 3.1 6.9 4 8 4C9.1 4 10 3.1 10 2C10 0.9 9.1 0 8 0Z"
            fill="currentColor"
          />
        </svg>
      </ButtonIcon>

      <ButtonIcon
        variant="danger-outline"
        title="Xóa ảnh"
        :height="28"
        :width="28"
        :min-width="28"
        @click="deleteImage"
      >
        <svg
          width="10"
          height="10"
          viewBox="0 0 10 10"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M9 1L1 9M1 1L9 9"
            stroke="#E61D1D"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </ButtonIcon>
    </ButtonGroup>
  </div>
</template>

<script setup>
import { computed, ref } from 'vue'
import { BASE_URL } from '@/services/axios-client.js'
import ButtonGroup from '@/components/controls/buttons/ButtonGroup.vue'
import Button from '@/components/controls/buttons/Button.vue'
import ButtonIcon from '@/components/controls/buttons/ButtonIcon.vue'
import ImgPlaceholder from '@/assets/icons/img_placeholder.png'
import { useToast } from '@/utils/use-toast'

const props = defineProps({
  modelValue: {
    type: String,
    default: null,
  },
})

const emit = defineEmits(['update:modelValue', 'file-change', 'menu'])

const { showToast } = useToast()

const imageUrl = computed(() => {
  if (!props.modelValue) {
    return null
  }
  // Nếu là ảnh vừa chọn (dạng blob) thì giữ nguyên
  if (props.modelValue.startsWith('blob:')) {
    return props.modelValue
  }
  // Nếu là đường dẫn từ server, ghép với BASE_URL
  // Ví dụ: https://localhost:7192/uploads/ten-anh.png
  return `${BASE_URL}/${props.modelValue}`
})

const fileInput = ref(null)

function triggerUpload() {
  fileInput.value?.click()
}

function onFileSelected(event) {
  const file = event.target.files[0]
  if (!file) {
    return
  }

  const allowedExtensions = ['.jpg', '.jpeg', '.png', '.gif']
  const fileName = file.name.toLowerCase()
  const fileExtension = fileName.substring(fileName.lastIndexOf('.'))

  if (!allowedExtensions.includes(fileExtension)) {
    showToast(
      'Định dạng tệp không hợp lệ. Vui lòng chọn ảnh có định dạng (.jpg, .jpeg, .png, .gif).',
      'error',
    )
    event.target.value = '' // Reset input
    return
  }

  const blobUrl = URL.createObjectURL(file)
  emit('update:modelValue', blobUrl)
  emit('file-change', file)
  event.target.value = ''
}

function deleteImage() {
  emit('update:modelValue', null)
  emit('file-change', null)
}
</script>

<style scoped>
.image-form {
  display: flex;
  flex-direction: column;
  background-color: #fff;
  gap: 8px;
  border-radius: 8px;
  align-items: left;
  text-align: left;
  width: 160px;
}

.image-preview {
  width: 160px;
  height: 120px;
  background-color: #f0f6fe;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}

.preview-img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.image-label {
  font-weight: 400;
  font-size: 13px;
  color: #111;
}

.image-help {
  font-weight: 400;
  font-size: 12px;
  color: #717680;
}

.image-actions {
  width: 100%;
  justify-content: center;
}
</style>
