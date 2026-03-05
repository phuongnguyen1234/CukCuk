/**
 * Hàm chuyển đổi định dạng ngày từ yyyy-MM-dd sang dd/MM/yyyy
 * Created by phuong 22/1/2026
 * @param {*} date Ngày định dạng yyyy-MM-dd
 * @returns Ngày định dạng dd/MM/yyyy
 */
function toDDMMYYYY(date) {
  let parts = date.split('-')
  return parts[2] + '/' + parts[1] + '/' + parts[0]
}

/**
 * Hàm chuyển đổi định dạng ngày từ dd/MM/yyyy sang yyyy-MM-dd
 * Created by phuong 22/1/2026
 * @param {*} date Ngày định dạng dd/MM/yyyy
 * @returns Ngày định dạng yyyy-MM-dd
 */
function toYYYYMMDD(date) {
  let parts = date.split('/')
  return parts[2] + '-' + parts[1] + '-' + parts[0]
}

export { toDDMMYYYY, toYYYYMMDD }
