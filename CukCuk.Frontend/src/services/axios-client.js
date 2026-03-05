import axios from 'axios'

// Cấu hình đường dẫn gốc cho API
// Hãy thay đổi port (ví dụ: 7166) phù hợp với launchSettings.json của Backend
export const BASE_URL = 'https://localhost:7192'

const apiClient = axios.create({
  baseURL: `${BASE_URL}/api/`,
  headers: {
    'Content-Type': 'application/json',
  },
  // Thời gian chờ tối đa (10s)
  timeout: 10000,
})

export default apiClient
