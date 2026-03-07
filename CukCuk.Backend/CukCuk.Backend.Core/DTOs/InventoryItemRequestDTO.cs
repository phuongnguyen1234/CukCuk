﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.DTOs
{
    /// <summary>
    /// DTO Request cho InventoryItem (Dùng cho Create/Update)
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class InventoryItemRequestDTO
    {
        /// <summary>
        /// Khóa chính của món (GUID).
        /// </summary>
        public Guid? InventoryItemId { get; set; }
        /// <summary>
        /// Id danh mục món (liên kết InventoryItemCategory).
        /// </summary>
        public Guid? InventoryItemCategoryId { get; set; }

        /// <summary>
        /// Id đơn vị tính (liên kết Unit).
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Danh sách Id bếp/bar chế biến (liên kết Kitchen).
        /// </summary>
        public IEnumerable<Guid>? KitchenIds { get; set; }

        /// <summary>
        /// Id loại món (liên kết InventoryItemType).
        /// </summary>
        public Guid InventoryItemTypeId { get; set; }

        /// <summary>
        /// Tên món hiển thị.
        /// </summary>
        public string InventoryItemName { get; set; } = null!;

        /// <summary>
        /// Mã món (duy nhất trong hệ thống).
        /// </summary>
        public string InventoryItemCode { get; set; } = null!;

        /// <summary>
        /// Tên món bằng ngôn ngữ khác (ví dụ: tiếng Anh).
        /// </summary>
        public string? InventoryItemLangName { get; set; }

        /// <summary>
        /// Thứ tự món trong thực đơn (khai vị, món chính, tráng miệng).
        /// 1 = Món khai vị,
        /// 2 = Món chính,
        /// 3 = Món tráng miệng,
        /// </summary>
        public byte InventoryItemCourse { get; set; }

        /// <summary>
        /// Có phải món đặc trưng (0/1).
        /// </summary>
        public bool? InventoryItemIsFeatured { get; set; }

        /// <summary>
        /// Giá bán của món.
        /// </summary>
        public decimal InventoryItemPrice { get; set; }

        /// <summary>
        /// Có phải giá thị trường (0/1).
        /// </summary>
        public bool? InventoryItemIsMarketPrice { get; set; }

        /// <summary>
        /// Cho phép chỉnh sửa giá khi bán (0/1).
        /// </summary>
        public bool? InventoryItemAllowPriceOverride { get; set; }

        /// <summary>
        /// Giá vốn của món (phục vụ tính lợi nhuận).
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
        /// Id của các addition (InventoryItemAddition) liên kết với món này.
        /// </summary>
        public IEnumerable<Guid>? AdditionIds { get; set; }
    }
}
