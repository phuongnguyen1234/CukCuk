using System;
using CukCuk.Backend.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.DTOs
{
    /// <summary>
    /// DTO Response cho InventoryItem (Dùng cho Get)
    /// </summary>
    /// Created by Phuong 28/02/2026
    public class InventoryItemResponseDTO
    {
        /// <summary>
        /// Khóa chính của món (GUID).
        /// </summary>
        public Guid InventoryItemId { get; set; }

        /// <summary>
        /// Id danh mục món.
        /// </summary>
        public Guid? InventoryItemCategoryId { get; set; }

        /// <summary>
        /// Tên danh mục món.
        /// </summary>
        public string? InventoryItemCategoryName { get; set; }

        /// <summary>
        /// Id đơn vị tính.
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tên đơn vị tính.
        /// </summary>
        public string? UnitName { get; set; }

        /// <summary>
        /// Danh sách bếp/bar chế biến.
        /// </summary>
        public IEnumerable<KitchenDTO>? Kitchens { get; set; }

        /// <summary>
        /// Id loại món.
        /// </summary>
        public Guid InventoryItemTypeId { get; set; }

        /// <summary>
        /// Tên loại món.
        /// </summary>
        public string? InventoryItemTypeName { get; set; }

        /// <summary>
        /// Tên món hiển thị.
        /// </summary>
        public string InventoryItemName { get; set; } = null!;

        /// <summary>
        /// Mã món.
        /// </summary>
        public string InventoryItemCode { get; set; } = null!;

        /// <summary>
        /// Tên món bằng ngôn ngữ khác.
        /// </summary>
        public string? InventoryItemLangName { get; set; }

        /// <summary>
        /// Thứ tự món trong thực đơn.
        /// </summary>
        public byte InventoryItemCourse { get; set; }

        /// <summary>
        /// Có phải món đặc trưng.
        /// </summary>
        public bool? InventoryItemIsFeatured { get; set; }

        /// <summary>
        /// Giá bán của món.
        /// </summary>
        public decimal InventoryItemPrice { get; set; }

        /// <summary>
        /// Có phải giá thị trường.
        /// </summary>
        public bool? InventoryItemIsMarketPrice { get; set; }

        /// <summary>
        /// Cho phép chỉnh sửa giá khi bán.
        /// </summary>
        public bool? InventoryItemAllowPriceOverride { get; set; }

        /// <summary>
        /// Giá vốn của món.
        /// </summary>
        public decimal InventoryItemCostPrice { get; set; }

        /// <summary>
        /// Mô tả chi tiết món ăn.
        /// </summary>
        public string? InventoryItemDescription { get; set; } = string.Empty;

        /// <summary>
        /// Đường dẫn ảnh của món.
        /// </summary>
        public string? InventoryItemImage { get; set; }

        /// <summary>
        /// Món có đang bán không
        /// </summary>
        public bool? InventoryItemIsOnSale { get; set; }

        /// <summary>
        /// Danh sách chi tiết các addition.
        /// </summary>
        public IEnumerable<InventoryItemAdditionDTO>? Additions { get; set; }
    }
}
