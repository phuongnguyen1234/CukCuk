import apiClient from '../services/axios-client'
import { BaseService } from '../services/base-service'

class InventoryItemService extends BaseService {
  constructor() {
    // Gọi constructor của cha với endpoint '/InventoryItem'
    super('/InventoryItem')
  }

  /**
   * Override getAll để hỗ trợ tham số search
   * @param {number} page
   * @param {number} pageSize
   * @param {Array} filters
   * @param {string} search - Từ khóa tìm kiếm
   */
  async getAll(page = 1, pageSize = 20, filters = [], search = '') {
    const queryParams = new URLSearchParams()
    queryParams.append('page', page)
    queryParams.append('pageSize', pageSize)

    if (search) queryParams.append('search', search)

    if (filters && filters.length > 0) {
      filters.forEach((f) => queryParams.append('filter', f))
    }

    const response = await apiClient.get(this.controllerEndpoint, { params: queryParams })
    return response.data
  }

  /**
   * Tạo mới món ăn (Có hỗ trợ upload ảnh)
   * Override lại method create của BaseService để dùng FormData
   * @param {Object} inventoryItem - Đối tượng chứa thông tin món
   * @param {File} file - File ảnh (nếu có)
   */
  async create(inventoryItem, file) {
    const formData = this.buildFormData(inventoryItem, file)

    // Cần set header là multipart/form-data để gửi file
    const response = await apiClient.post(this.controllerEndpoint, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    })
    return response.data
  }

  /**
   * Cập nhật món ăn
   * Override lại method update của BaseService để dùng FormData
   * @param {string} id
   * @param {Object} inventoryItem
   * @param {File} file
   */
  async update(id, inventoryItem, file) {
    const formData = this.buildFormData(inventoryItem, file)

    const response = await apiClient.put(`${this.controllerEndpoint}/${id}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    })
    return response.data
  }

  /**
   * Sinh mã món ăn tự động
   * @param {string} name
   */
  async generateCode(name) {
    const response = await apiClient.get(`${this.controllerEndpoint}/generate-code`, {
      params: { name: name },
    })
    return response.data
  }

  /**
   * Helper: Chuyển object sang FormData để gửi lên Controller [FromForm]
   */
  buildFormData(item, file) {
    const formData = new FormData()

    // Duyệt qua các key của object item và append vào formData
    for (const key in item) {
      if (item[key] !== null && item[key] !== undefined) {
        // Nếu là mảng (ví dụ KitchenIds, AdditionIds), cần append từng phần tử
        if (Array.isArray(item[key])) {
          item[key].forEach((val) => formData.append(key, val))
        } else {
          formData.append(key, item[key])
        }
      }
    }

    // Append file ảnh nếu có (tên tham số phải khớp với Controller: IFormFile? image)
    if (file) {
      formData.append('image', file)
    }

    return formData
  }
}

export default new InventoryItemService()
