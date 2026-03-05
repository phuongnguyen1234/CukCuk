/**
 * Format số thành tiền tệ (VND)
 * @param {number|string} value - Giá trị cần format
 * @returns {string} - Chuỗi đã format (VD: 100.000)
 */
export const formatCurrency = (value) => {
  if (value === null || value === undefined || value === '') return ''
  // Sử dụng Intl.NumberFormat để format chuẩn tiếng Việt (dùng dấu chấm phân cách hàng nghìn)
  return new Intl.NumberFormat('vi-VN').format(value)
}
