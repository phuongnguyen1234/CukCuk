import apiClient from './axios-client'

/**
 * Class cha cung cấp các method CRUD cơ bản gọi API.
 * Các service con sẽ kế thừa class này.
 */
export class BaseService {
  /**
   * Khởi tạo service với endpoint controller tương ứng
   * @param {string} controllerEndpoint - Ví dụ: '/InventoryItem'
   */
  constructor(controllerEndpoint) {
    this.controllerEndpoint = controllerEndpoint
  }

  /**
   * Lấy danh sách có phân trang và lọc
   * @param {number} page
   * @param {number} pageSize
   * @param {Array} filters
   */
  async getAll(page = 1, pageSize = 20, filters = []) {
    // Sử dụng URLSearchParams để xử lý mảng filter (filter=A&filter=B)
    const queryParams = new URLSearchParams()
    queryParams.append('page', page)
    queryParams.append('pageSize', pageSize)

    if (filters && filters.length > 0) {
      filters.forEach((f) => queryParams.append('filter', f))
    }

    const response = await apiClient.get(this.controllerEndpoint, { params: queryParams })
    return response.data
  }

  /**
   * Lấy chi tiết theo ID
   * @param {string} id
   */
  async getById(id) {
    const response = await apiClient.get(`${this.controllerEndpoint}/${id}`)
    return response.data
  }

  /**
   * Tạo mới (Mặc định gửi JSON)
   * @param {Object} data
   */
  async create(data) {
    const response = await apiClient.post(this.controllerEndpoint, data)
    return response.data
  }

  /**
   * Cập nhật (Mặc định gửi JSON)
   * @param {string} id
   * @param {Object} data
   */
  async update(id, data) {
    const response = await apiClient.put(`${this.controllerEndpoint}/${id}`, data)
    return response.data
  }

  /**
   * Xóa
   * @param {string} id
   */
  async delete(id) {
    const response = await apiClient.delete(`${this.controllerEndpoint}/${id}`)
    return response.data
  }
}
