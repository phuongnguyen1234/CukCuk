/**
 * Chuyển đổi chuỗi từ PascalCase/CamelCase sang snake_case
 * VD: inventoryItemName -> inventory_item_name
 * @param {string} str
 * @returns {string}
 */
export function pascalToSnakeCase(str) {
  if (!str) return ''
  return str.replace(/[A-Z]/g, (letter) => `_${letter.toLowerCase()}`)
}
