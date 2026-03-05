import { ref } from 'vue'

// State global chứa danh sách toast
const toasts = ref([])

export function useToast() {
  /**
   * Hiển thị một thông báo toast.
   * @param {string} message - Nội dung thông báo.
   * @param {string} type - Loại thông báo ('success', 'error', hoặc 'warning').
   * @param {number} duration - Thời gian hiển thị (ms).
   */
  const showToast = (message, type = 'success', duration = 3000) => {
    const id = Date.now() // Tạo ID duy nhất

    const toast = {
      id,
      message,
      type,
      duration,
    }

    toasts.value.push(toast)

    // Tự động xóa sau thời gian duration
    if (duration > 0) {
      setTimeout(() => {
        removeToast(id)
      }, duration)
    }
  }

  /**
   * Xóa toast khỏi danh sách
   * @param {number} id
   */
  const removeToast = (id) => {
    toasts.value = toasts.value.filter((t) => t.id !== id)
  }

  return {
    toasts,
    showToast,
    removeToast,
  }
}
