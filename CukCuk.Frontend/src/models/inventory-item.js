export default class InventoryItem {
  constructor({
    inventoryItemId = null,
    inventoryItemCategoryId = null,
    inventoryItemCategoryName = '',
    unitId = null,
    unitName = '',
    kitchenIds = [],
    kitchens = [],
    inventoryItemTypeId = null,
    inventoryItemTypeName = '',
    inventoryItemName = '',
    inventoryItemCode = '',
    inventoryItemLangName = '',
    inventoryItemCourse = 0, // 1: Khai vị, 2: Món chính, 3: Tráng miệng, 4: Đồ uống
    inventoryItemIsFeatured = false,
    inventoryItemPrice = 0,
    inventoryItemIsMarketPrice = false,
    inventoryItemAllowPriceOverride = false,
    inventoryItemCostPrice = null,
    inventoryItemDescription = '',
    inventoryItemImage = '',
    inventoryItemIsOnSale = true,
    additionIds = [],
    additions = [],
  } = {}) {
    this.inventoryItemId = inventoryItemId
    this.inventoryItemCategoryId = inventoryItemCategoryId
    this.inventoryItemCategoryName = inventoryItemCategoryName
    this.unitId = unitId
    this.unitName = unitName
    this.kitchenIds = kitchenIds
    this.kitchens = kitchens
    this.inventoryItemTypeId = inventoryItemTypeId
    this.inventoryItemTypeName = inventoryItemTypeName
    this.inventoryItemName = inventoryItemName
    this.inventoryItemCode = inventoryItemCode
    this.inventoryItemLangName = inventoryItemLangName
    this.inventoryItemCourse = inventoryItemCourse
    this.inventoryItemIsFeatured = inventoryItemIsFeatured
    this.inventoryItemPrice = inventoryItemPrice
    this.inventoryItemIsMarketPrice = inventoryItemIsMarketPrice
    this.inventoryItemAllowPriceOverride = inventoryItemAllowPriceOverride
    this.inventoryItemCostPrice = inventoryItemCostPrice
    this.inventoryItemDescription = inventoryItemDescription
    this.inventoryItemImage = inventoryItemImage
    this.inventoryItemIsOnSale = inventoryItemIsOnSale

    // Danh sách ID sở thích (để gửi lên server khi thêm/sửa)
    this.additionIds = additionIds
    // Danh sách chi tiết sở thích (để hiển thị)
    this.additions = additions
  }
}
