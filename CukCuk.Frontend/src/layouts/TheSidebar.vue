<template>
  <!--sidebar-->
  <div class="sidebar sidebar_active" :class="{ sidebar_expanded: isExpanded }">
    <!--sidebar menu-->
    <div class="sidebar_menu">
      <!--tổng quan-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'dashboard' }"
        @click="router.push({ name: 'dashboard' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="17"
            height="15"
            viewBox="0 0 17 15"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M9.45834 7.07964L11.1667 5.3713M6.58333 8.28797C6.58333 8.73 6.75893 9.15392 7.07149 9.46648C7.38405 9.77904 7.80797 9.95464 8.25 9.95464C8.69203 9.95464 9.11595 9.77904 9.42851 9.46648C9.74107 9.15392 9.91667 8.73 9.91667 8.28797C9.91667 7.84594 9.74107 7.42202 9.42851 7.10946C9.11595 6.7969 8.69203 6.6213 8.25 6.6213C7.80797 6.6213 7.38405 6.7969 7.07149 7.10946C6.75893 7.42202 6.58333 7.84594 6.58333 8.28797ZM3.58333 14.1213C2.36512 13.153 1.47826 11.8298 1.04565 10.335C0.613037 8.84022 0.6561 7.24787 1.16887 5.77864C1.68164 4.30941 2.63873 3.03606 3.90749 2.13505C5.17625 1.23404 6.69386 0.75 8.25 0.75C9.80614 0.75 11.3237 1.23404 12.5925 2.13505C13.8613 3.03606 14.8184 4.30941 15.3311 5.77864C15.8439 7.24787 15.887 8.84022 15.4544 10.335C15.0217 11.8298 14.1349 13.153 12.9167 14.1213H3.58333Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Tin tuyển dụng</div>
      </div>

      <!--báo cáo-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'report' }"
        @click="router.push({ name: 'report' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="17"
            height="17"
            viewBox="0 0 17 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M6.55624 0.755113C5.25266 1.05731 4.05368 1.70278 3.08364 2.62456C2.11361 3.54634 1.40786 4.71086 1.0396 5.99734C0.67134 7.28382 0.653979 8.6454 0.989319 9.94085C1.32466 11.2363 2.00048 12.4184 2.9467 13.3647C3.89291 14.3109 5.07505 14.9867 6.3705 15.322C7.66595 15.6574 9.02753 15.64 10.314 15.2718C11.6005 14.9035 12.765 14.1977 13.6868 13.2277C14.6086 12.2577 15.254 11.0587 15.5562 9.75511C15.5562 9.5341 15.4684 9.32214 15.3122 9.16586C15.1559 9.00958 14.9439 8.92178 14.7229 8.92178H9.05624C8.61422 8.92178 8.19029 8.74618 7.87773 8.43362C7.56517 8.12106 7.38958 7.69714 7.38958 7.25511V1.42178C7.37922 1.32305 7.34937 1.22736 7.30176 1.14025C7.25414 1.05315 7.1897 0.976362 7.11219 0.914348C7.03467 0.852334 6.94561 0.806327 6.85018 0.778992C6.75475 0.751658 6.65484 0.743541 6.55624 0.755113Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M10.7229 1.00511C11.781 1.37768 12.7421 1.98286 13.5353 2.77608C14.3285 3.5693 14.9337 4.53035 15.3062 5.58845H11.5562C11.3352 5.58845 11.1233 5.50065 10.967 5.34437C10.8107 5.18809 10.7229 4.97613 10.7229 4.75511V1.00511Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Báo cáo</div>
        <div class="sidebar_menu_arrow">
          <ArrowDownIcon size="10" />
        </div>
      </div>

      <!--hóa đơn-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'invoice' }"
        @click="router.push({ name: 'invoice' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="14"
            height="17"
            viewBox="0 0 14 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M4.08333 4.08333H9.08333M4.08333 7.41667H9.08333M7.41667 10.75H9.08333M0.75 15.75V2.41667C0.75 1.97464 0.925595 1.55072 1.23816 1.23816C1.55072 0.925595 1.97464 0.75 2.41667 0.75H10.75C11.192 0.75 11.616 0.925595 11.9285 1.23816C12.2411 1.55072 12.4167 1.97464 12.4167 2.41667V15.75L9.91667 14.0833L8.25 15.75L6.58333 14.0833L4.91667 15.75L3.25 14.0833L0.75 15.75Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Hóa đơn</div>
        <div class="sidebar_menu_arrow">
          <ArrowDownIcon size="10" />
        </div>
      </div>

      <!--kho-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'warehouse' }"
        @click="router.push({ name: 'warehouse' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="19"
            height="18"
            viewBox="0 0 19 18"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M17.4167 7.41667L9.81875 1.01839C9.61292 0.845058 9.35242 0.75 9.08333 0.75C8.81425 0.75 8.55375 0.845058 8.34792 1.01839L0.75 7.41667M16.1667 2.83333V11.5833C16.1667 13.9403 16.1667 15.1188 15.4344 15.8511C14.7022 16.5833 13.5237 16.5833 11.1667 16.5833H7C4.64298 16.5833 3.46447 16.5833 2.73223 15.8511C2 15.1188 2 13.9403 2 11.5833V6.58333M7.83333 8.25H7V9.08333H7.83333V8.25ZM11.1667 8.25H10.3333V9.08333H11.1667V8.25ZM7.83333 11.5833H7V12.4167H7.83333V11.5833ZM11.1667 11.5833H10.3333V12.4167H11.1667V11.5833Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Kho</div>
        <div class="sidebar_menu_arrow">
          <ArrowDownIcon size="10" />
        </div>
      </div>

      <!--Quỹ tiền-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'fund' }"
        @click="router.push({ name: 'fund' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="15"
            height="17"
            viewBox="0 0 15 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M5.33333 0.75H9.5C9.83152 0.75 10.1495 0.881696 10.3839 1.11612C10.6183 1.35054 10.75 1.66848 10.75 2C10.75 2.77355 10.4427 3.51541 9.89573 4.06239C9.34875 4.60938 8.60688 4.91667 7.83333 4.91667H7C6.22645 4.91667 5.48459 4.60938 4.93761 4.06239C4.39062 3.51541 4.08333 2.77355 4.08333 2C4.08333 1.66848 4.21503 1.35054 4.44945 1.11612C4.68387 0.881696 5.00181 0.75 5.33333 0.75Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M0.75 12.4167V11.5833C0.75 9.81522 1.45238 8.11953 2.70262 6.86929C3.95286 5.61905 5.64856 4.91667 7.41667 4.91667C9.18478 4.91667 10.8805 5.61905 12.1307 6.86929C13.381 8.11953 14.0833 9.81522 14.0833 11.5833V12.4167C14.0833 13.3007 13.7321 14.1486 13.107 14.7737C12.4819 15.3988 11.6341 15.75 10.75 15.75H4.08333C3.19928 15.75 2.35143 15.3988 1.72631 14.7737C1.10119 14.1486 0.75 13.3007 0.75 12.4167Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Quỹ tiền</div>
        <div class="sidebar_menu_arrow">
          <ArrowDownIcon size="10" />
        </div>
      </div>

      <!--divider-->
      <div class="sidebar_menu_divider"></div>

      <!--thực đơn-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'menu' }"
        @click="router.push({ name: 'menu' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="17"
            height="17"
            viewBox="0 0 17 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M15.75 13.1667C16.1642 13.1667 16.5 12.8309 16.5 12.4167C16.5 12.0025 16.1642 11.6667 15.75 11.6667V12.4167V13.1667ZM15.75 4.83333C16.1642 4.83333 16.5 4.49755 16.5 4.08333C16.5 3.66912 16.1642 3.33333 15.75 3.33333V4.08333V4.83333ZM15.75 16.5C16.1642 16.5 16.5 16.1642 16.5 15.75C16.5 15.3358 16.1642 15 15.75 15V15.75V16.5ZM15.75 1.5C16.1642 1.5 16.5 1.16421 16.5 0.75C16.5 0.335786 16.1642 0 15.75 0V0.75V1.5ZM4 0.75C4 0.335786 3.66421 0 3.25 0C2.83579 0 2.5 0.335786 2.5 0.75H3.25H4ZM2.5 4.91667C2.5 5.33088 2.83579 5.66667 3.25 5.66667C3.66421 5.66667 4 5.33088 4 4.91667H3.25H2.5ZM2.5 15.75C2.5 16.1642 2.83579 16.5 3.25 16.5C3.66421 16.5 4 16.1642 4 15.75H3.25H2.5ZM4 7.41667C4 7.00245 3.66421 6.66667 3.25 6.66667C2.83579 6.66667 2.5 7.00245 2.5 7.41667H3.25H4ZM1.16667 4.16667C0.752453 4.16667 0.416667 4.50245 0.416667 4.91667C0.416667 5.33088 0.752453 5.66667 1.16667 5.66667V4.91667V4.16667ZM5.33333 5.66667C5.74755 5.66667 6.08333 5.33088 6.08333 4.91667C6.08333 4.50245 5.74755 4.16667 5.33333 4.16667V4.91667V5.66667ZM6.5 0.75C6.5 0.335786 6.16421 0 5.75 0C5.33579 0 5 0.335786 5 0.75H5.75H6.5ZM1.5 0.75C1.5 0.335786 1.16421 0 0.75 0C0.335786 0 0 0.335786 0 0.75H0.75H1.5ZM15.75 12.4167V11.6667C13.863 11.6667 12.3333 10.137 12.3333 8.25H11.5833H10.8333C10.8333 10.9654 13.0346 13.1667 15.75 13.1667V12.4167ZM11.5833 8.25H12.3333C12.3333 6.36303 13.863 4.83333 15.75 4.83333V4.08333V3.33333C13.0346 3.33333 10.8333 5.5346 10.8333 8.25H11.5833ZM15.75 15.75V15C12.022 15 9 11.978 9 8.25H8.25H7.5C7.5 12.8064 11.1936 16.5 15.75 16.5V15.75ZM8.25 8.25H9C9 4.52208 12.022 1.5 15.75 1.5V0.75V0C11.1936 0 7.5 3.69366 7.5 8.25H8.25ZM3.25 0.75H2.5V4.91667H3.25H4V0.75H3.25ZM3.25 15.75H4V7.41667H3.25H2.5V15.75H3.25ZM1.16667 4.91667V5.66667H5.33333V4.91667V4.16667H1.16667V4.91667ZM5.75 0.75H5V4.37687H5.75H6.5V0.75H5.75ZM5.75 4.37687H5C5 5.23234 4.73911 5.80058 4.41824 6.14739C4.09443 6.49737 3.67043 6.66668 3.25002 6.66667C2.8296 6.66666 2.40559 6.49733 2.08177 6.14732C1.76089 5.80049 1.5 5.23221 1.5 4.37672H0.75H0C0 5.54783 0.364111 6.49952 0.980725 7.16599C1.59441 7.82931 2.4204 8.16665 3.24998 8.16667C4.07957 8.16669 4.90557 7.82938 5.51926 7.16609C6.13589 6.49963 6.5 5.54796 6.5 4.37687H5.75ZM0.75 4.37672H1.5V0.75H0.75H0V4.37672H0.75Z"
              fill="currentColor"
            />
          </svg>
        </div>
        <div class="menu_text">Thực đơn</div>
      </div>

      <!--Khuyến mại-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'promotion' }"
        @click="router.push({ name: 'promotion' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="17"
            height="17"
            viewBox="0 0 17 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M8.25 4.91702V15.7504M8.25 4.91702C7.94856 3.67486 7.42954 2.61292 6.76061 1.86966C6.09169 1.12641 5.3039 0.736358 4.5 0.750364C3.94747 0.750364 3.41756 0.969858 3.02686 1.36056C2.63616 1.75126 2.41667 2.28116 2.41667 2.8337C2.41667 3.38623 2.63616 3.91614 3.02686 4.30684C3.41756 4.69754 3.94747 4.91703 4.5 4.91703M8.25 4.91702C8.55144 3.67486 9.07046 2.61292 9.73939 1.86966C10.4083 1.12641 11.1961 0.736358 12 0.750364C12.5525 0.750364 13.0824 0.969858 13.4731 1.36056C13.8638 1.75126 14.0833 2.28116 14.0833 2.8337C14.0833 3.38623 13.8638 3.91614 13.4731 4.30684C13.0824 4.69754 12.5525 4.91703 12 4.91703M14.0833 8.25036V14.0837C14.0833 14.5257 13.9077 14.9496 13.5952 15.2622C13.2826 15.5748 12.8587 15.7504 12.4167 15.7504H4.08333C3.64131 15.7504 3.21738 15.5748 2.90482 15.2622C2.59226 14.9496 2.41667 14.5257 2.41667 14.0837V8.25036M0.75 5.75036C0.75 5.52934 0.837797 5.31738 0.994078 5.1611C1.15036 5.00482 1.36232 4.91702 1.58333 4.91702H14.9167C15.1377 4.91702 15.3496 5.00482 15.5059 5.1611C15.6622 5.31738 15.75 5.52934 15.75 5.75036V7.41702C15.75 7.63804 15.6622 7.85 15.5059 8.00628C15.3496 8.16256 15.1377 8.25036 14.9167 8.25036H1.58333C1.36232 8.25036 1.15036 8.16256 0.994078 8.00628C0.837797 7.85 0.75 7.63804 0.75 7.41702V5.75036Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Khuyến mại</div>
      </div>

      <!--Gọi món tại bàn-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'table-order' }"
        @click="router.push({ name: 'table-order' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="15"
            height="15"
            viewBox="0 0 15 15"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M3.25 11.5833V11.5917M3.25 3.25V3.25833M11.5833 3.25V3.25833M11.5833 9.08333H9.08333V11.5833M14.0833 9.08333V9.09167M9.08333 14.0833H11.5833M11.5833 11.5833H14.0833V14.0833M0.75 1.58333C0.75 1.36232 0.837798 1.15036 0.994078 0.994078C1.15036 0.837798 1.36232 0.75 1.58333 0.75H4.91667C5.13768 0.75 5.34964 0.837798 5.50592 0.994078C5.6622 1.15036 5.75 1.36232 5.75 1.58333V4.91667C5.75 5.13768 5.6622 5.34964 5.50592 5.50592C5.34964 5.6622 5.13768 5.75 4.91667 5.75H1.58333C1.36232 5.75 1.15036 5.6622 0.994078 5.50592C0.837798 5.34964 0.75 5.13768 0.75 4.91667V1.58333ZM9.08333 1.58333C9.08333 1.36232 9.17113 1.15036 9.32741 0.994078C9.48369 0.837798 9.69565 0.75 9.91667 0.75H13.25C13.471 0.75 13.683 0.837798 13.8393 0.994078C13.9955 1.15036 14.0833 1.36232 14.0833 1.58333V4.91667C14.0833 5.13768 13.9955 5.34964 13.8393 5.50592C13.683 5.6622 13.471 5.75 13.25 5.75H9.91667C9.69565 5.75 9.48369 5.6622 9.32741 5.50592C9.17113 5.34964 9.08333 5.13768 9.08333 4.91667V1.58333ZM0.75 9.91667C0.75 9.69565 0.837798 9.48369 0.994078 9.32741C1.15036 9.17113 1.36232 9.08333 1.58333 9.08333H4.91667C5.13768 9.08333 5.34964 9.17113 5.50592 9.32741C5.6622 9.48369 5.75 9.69565 5.75 9.91667V13.25C5.75 13.471 5.6622 13.683 5.50592 13.8393C5.34964 13.9955 5.13768 14.0833 4.91667 14.0833H1.58333C1.36232 14.0833 1.15036 13.9955 0.994078 13.8393C0.837798 13.683 0.75 13.471 0.75 13.25V9.91667Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Gọi món tại bàn</div>
      </div>

      <!--Bán hàng online-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'online-sale' }"
        @click="router.push({ name: 'online-sale' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="17"
            height="17"
            viewBox="0 0 17 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M1.25 5.75009H15.25M1.25 10.7501H15.25M7.83333 0.750092C6.42945 2.99977 5.68517 5.59831 5.68517 8.25009C5.68517 10.9019 6.42945 13.5004 7.83333 15.7501M8.66667 0.750092C10.0705 2.99977 10.8148 5.59831 10.8148 8.25009C10.8148 10.9019 10.0705 13.5004 8.66667 15.7501M0.75 8.25009C0.75 9.23501 0.943993 10.2103 1.3209 11.1202C1.69781 12.0302 2.25026 12.857 2.9467 13.5534C3.64314 14.2498 4.46993 14.8023 5.37987 15.1792C6.28982 15.5561 7.26509 15.7501 8.25 15.7501C9.23491 15.7501 10.2102 15.5561 11.1201 15.1792C12.0301 14.8023 12.8569 14.2498 13.5533 13.5534C14.2497 12.857 14.8022 12.0302 15.1791 11.1202C15.556 10.2103 15.75 9.23501 15.75 8.25009C15.75 6.26097 14.9598 4.35331 13.5533 2.94679C12.1468 1.54027 10.2391 0.750092 8.25 0.750092C6.26088 0.750092 4.35322 1.54027 2.9467 2.94679C1.54018 4.35331 0.75 6.26097 0.75 8.25009Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Bán hàng online</div>
      </div>

      <!--divider-->
      <div class="sidebar_menu_divider"></div>

      <!--Kê khai thuế-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'tax-filing' }"
        @click="router.push({ name: 'tax-filing' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="14"
            height="17"
            viewBox="0 0 14 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M4.08333 9.91667L9.08333 4.91667M4.91667 5.33333C4.91667 5.56345 4.73012 5.75 4.5 5.75C4.26988 5.75 4.08333 5.56345 4.08333 5.33333C4.08333 5.10321 4.26988 4.91667 4.5 4.91667C4.73012 4.91667 4.91667 5.10321 4.91667 5.33333ZM9.08333 9.5C9.08333 9.73012 8.89679 9.91667 8.66667 9.91667C8.43655 9.91667 8.25 9.73012 8.25 9.5C8.25 9.26988 8.43655 9.08333 8.66667 9.08333C8.89679 9.08333 9.08333 9.26988 9.08333 9.5ZM0.75 15.75V2.41667C0.75 1.97464 0.925595 1.55072 1.23816 1.23816C1.55072 0.925595 1.97464 0.75 2.41667 0.75H10.75C11.192 0.75 11.616 0.925595 11.9285 1.23816C12.2411 1.55072 12.4167 1.97464 12.4167 2.41667V15.75L9.91667 14.0833L8.25 15.75L6.58333 14.0833L4.91667 15.75L3.25 14.0833L0.75 15.75Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Kê khai thuế</div>
      </div>

      <!--divider-->
      <div class="sidebar_menu_divider"></div>

      <!--MISA Lomas-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'misa-lomas' }"
        @click="router.push({ name: 'misa-lomas' })"
      >
        <div class="sidebar_menu_misa_lomas_icon"></div>
        <div class="menu_text">MISA Lomas</div>
      </div>

      <!--divider-->
      <div class="sidebar_menu_divider"></div>

      <!--Danh mục-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'category' }"
        @click="router.push({ name: 'category' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="17"
            height="15"
            viewBox="0 0 17 15"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M5 1H16M5 7H16M5 13H16M1 1V1.00999M1 7V7.00999M1 13V13.01"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Danh mục</div>
      </div>

      <!--Thiết lập hệ thống-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'setting' }"
        @click="router.push({ name: 'setting' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="20"
            height="20"
            viewBox="0 0 20 20"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M8.325 2.317C8.751 0.561 11.249 0.561 11.675 2.317C11.7389 2.581 11.8642 2.82597 12.0407 3.03197C12.2172 3.23797 12.4399 3.40001 12.6907 3.50401C12.9414 3.60801 13.2132 3.65099 13.4838 3.62999C13.7544 3.60899 14.0162 3.52398 14.248 3.38298C15.791 2.44298 17.558 4.20897 16.618 5.75297C16.4769 5.98497 16.3924 6.24601 16.3715 6.51701C16.3506 6.78701 16.3938 7.059 16.4975 7.309C16.6013 7.56 16.7627 7.78297 16.9687 7.95897C17.1747 8.13597 17.4194 8.261 17.683 8.325C19.439 8.751 19.439 11.249 17.683 11.675C17.4192 11.739 17.1742 11.864 16.968 12.041C16.7618 12.217 16.6001 12.44 16.4963 12.691C16.3924 12.941 16.3491 13.213 16.3701 13.484C16.3911 13.754 16.4757 14.016 16.617 14.248C17.557 15.791 15.791 17.558 14.247 16.618C14.0153 16.477 13.7537 16.392 13.4832 16.372C13.2128 16.351 12.9412 16.394 12.6906 16.498C12.44 16.601 12.2174 16.763 12.0409 16.969C11.8645 17.175 11.7391 17.419 11.675 17.683C11.249 19.439 8.751 19.439 8.325 17.683C8.2611 17.419 8.1358 17.174 7.9593 16.968C7.7828 16.762 7.5601 16.6 7.3093 16.496C7.0586 16.392 6.7868 16.349 6.5162 16.37C6.2456 16.391 5.9838 16.476 5.752 16.617C4.209 17.557 2.442 15.791 3.382 14.247C3.5231 14.015 3.6076 13.754 3.6285 13.483C3.6494 13.213 3.6062 12.941 3.5025 12.691C3.3987 12.44 3.2373 12.217 3.0313 12.041C2.8253 11.864 2.5806 11.739 2.317 11.675C0.561 11.249 0.561 8.751 2.317 8.325C2.5808 8.261 2.8258 8.13597 3.032 7.95897C3.2382 7.78297 3.3999 7.56 3.5037 7.309C3.6076 7.059 3.6509 6.78697 3.6299 6.51597C3.6089 6.24597 3.5243 5.984 3.383 5.752C2.443 4.209 4.209 2.442 5.753 3.382C6.753 3.99 8.049 3.452 8.325 2.317Z"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
            <path
              d="M7 10C7 10.796 7.3161 11.559 7.8787 12.121C8.4413 12.684 9.2044 13 10 13C10.7956 13 11.5587 12.684 12.1213 12.121C12.6839 11.559 13 10.796 13 10C13 9.204 12.6839 8.44103 12.1213 7.87903C11.5587 7.31603 10.7956 7 10 7C9.2044 7 8.4413 7.31603 7.8787 7.87903C7.3161 8.44103 7 9.204 7 10Z"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Thiết lập hệ thống</div>
      </div>

      <!--Ứng dụng-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'applications' }"
        @click="router.push({ name: 'applications' })"
      >
        <div class="sidebar_menu_icon">
          <svg
            width="15"
            height="15"
            viewBox="0 0 15 15"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M9.08333 3.25H14.0833M11.5833 0.75V5.75M0.75 1.58333C0.75 1.36232 0.837798 1.15036 0.994078 0.994078C1.15036 0.837798 1.36232 0.75 1.58333 0.75H4.91667C5.13768 0.75 5.34964 0.837798 5.50592 0.994078C5.6622 1.15036 5.75 1.36232 5.75 1.58333V4.91667C5.75 5.13768 5.6622 5.34964 5.50592 5.50592C5.34964 5.6622 5.13768 5.75 4.91667 5.75H1.58333C1.36232 5.75 1.15036 5.6622 0.994078 5.50592C0.837798 5.34964 0.75 5.13768 0.75 4.91667V1.58333ZM0.75 9.91667C0.75 9.69565 0.837798 9.48369 0.994078 9.32741C1.15036 9.17113 1.36232 9.08333 1.58333 9.08333H4.91667C5.13768 9.08333 5.34964 9.17113 5.50592 9.32741C5.6622 9.48369 5.75 9.69565 5.75 9.91667V13.25C5.75 13.471 5.6622 13.683 5.50592 13.8393C5.34964 13.9955 5.13768 14.0833 4.91667 14.0833H1.58333C1.36232 14.0833 1.15036 13.9955 0.994078 13.8393C0.837798 13.683 0.75 13.471 0.75 13.25V9.91667ZM9.08333 9.91667C9.08333 9.69565 9.17113 9.48369 9.32741 9.32741C9.48369 9.17113 9.69565 9.08333 9.91667 9.08333H13.25C13.471 9.08333 13.683 9.17113 13.8393 9.32741C13.9955 9.48369 14.0833 9.69565 14.0833 9.91667V13.25C14.0833 13.471 13.9955 13.683 13.8393 13.8393C13.683 13.9955 13.471 14.0833 13.25 14.0833H9.91667C9.69565 14.0833 9.48369 13.9955 9.32741 13.8393C9.17113 13.683 9.08333 13.471 9.08333 13.25V9.91667Z"
              stroke="currentColor"
              stroke-width="1.5"
              stroke-linecap="round"
              stroke-linejoin="round"
            />
          </svg>
        </div>
        <div class="menu_text">Ứng dụng</div>
      </div>

      <!--Kết nối vay vốn-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'loan-connection' }"
        @click="router.push({ name: 'loan-connection' })"
      >
        <div class="sidebar_menu_loan_icon"></div>
        <div class="menu_text">Kết nối vay vốn</div>
      </div>

      <!--Kế toán dịch vụ-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'accounting-service' }"
        @click="router.push({ name: 'accounting-service' })"
      >
        <div class="sidebar_menu_accounting_service_icon"></div>
        <div class="menu_text">Kế toán dịch vụ</div>
      </div>

      <!--Sản phẩm-->
      <div
        class="sidebar_menu_item"
        :class="{ active: route.name === 'product' }"
        @click="router.push({ name: 'product' })"
      >
        <div class="sidebar_menu_product_icon"></div>
        <div class="menu_text">Sản phẩm</div>
      </div>
    </div>

    <!--sidebar toggle button-->
    <Button
      class="sidebar_toggle"
      variant="outline"
      :active="isExpanded"
      :min-width="40"
      height="40px"
      @click="emit('toggle')"
    >
      <template #icon>
        <!-- Right arrow icon -->
        <svg
          width="17"
          height="10"
          viewBox="0 0 17 10"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M9.08333 0.75H15.75M9.08333 9.08333H15.75M12.4167 4.91667H15.75M0.75 4.91667H9.08333M0.75 4.91667L4.5 8.66667M0.75 4.91667L4.5 1.16667"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </template>
      <template #icon-active>
        <!-- Left arrow icon -->
        <svg
          fill="currentColor"
          version="1.1"
          id="Layer_1"
          xmlns:x="&amp;ns_extend;"
          xmlns:i="&amp;ns_ai;"
          xmlns:graph="&amp;ns_graphs;"
          xmlns="http://www.w3.org/2000/svg"
          xmlns:xlink="http://www.w3.org/1999/xlink"
          width="64px"
          height="64px"
          viewBox="0 0 24 24"
          enable-background="new 0 0 24 24"
          xml:space="preserve"
        >
          <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
          <g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g>
          <g id="SVGRepo_iconCarrier">
            <metadata>
              <sfw xmlns="&amp;ns_sfw;">
                <slices></slices>
                <slicesourcebounds
                  width="505"
                  height="984"
                  bottomleftorigin="true"
                  x="0"
                  y="-120"
                ></slicesourcebounds>
              </sfw>
            </metadata>
            <g>
              <g>
                <g>
                  <path
                    d="M20,24H4c-2.2,0-4-1.8-4-4V4c0-2.2,1.8-4,4-4h16c2.2,0,4,1.8,4,4v16C24,22.2,22.2,24,20,24z M4,2C2.9,2,2,2.9,2,4v16 c0,1.1,0.9,2,2,2h16c1.1,0,2-0.9,2-2V4c0-1.1-0.9-2-2-2H4z"
                  ></path>
                </g>
              </g>
              <g>
                <g>
                  <path
                    d="M8,24c-0.6,0-1-0.4-1-1V1c0-0.6,0.4-1,1-1s1,0.4,1,1v22C9,23.6,8.6,24,8,24z"
                  ></path>
                </g>
              </g>
              <g>
                <g>
                  <path
                    d="M14,13c-0.3,0-0.5-0.1-0.7-0.3c-0.4-0.4-0.4-1,0-1.4l3-3c0.4-0.4,1-0.4,1.4,0s0.4,1,0,1.4l-3,3C14.5,12.9,14.3,13,14,13z "
                  ></path>
                </g>
              </g>
              <g>
                <g>
                  <path
                    d="M17,16c-0.3,0-0.5-0.1-0.7-0.3l-3-3c-0.4-0.4-0.4-1,0-1.4s1-0.4,1.4,0l3,3c0.4,0.4,0.4,1,0,1.4C17.5,15.9,17.3,16,17,16z "
                  ></path>
                </g>
              </g>
            </g>
          </g>
        </svg>
      </template>
      <div v-if="isExpanded" class="sidebar_toggle_text">Thu gọn</div>
    </Button>
  </div>
</template>

<style scoped>
/*style sidebar*/
.sidebar {
  position: absolute;
  top: 48px;
  width: 72px;
  bottom: 0;
  left: 0;
  padding: 16px 0 16px;
  background-color: #fff;
  box-sizing: border-box;
  display: none;
  background-position: bottom;
  background-repeat: no-repeat;
  background-size: cover;
  transition: width 0.3s ease;
  border-right: 1px solid #e9eaeb;
}

.sidebar_active {
  display: flex !important;
  flex-direction: column;
}

.sidebar_expanded {
  width: 200px;
}

.sidebar_menu {
  flex: 1;
  overflow-y: auto;
  overflow-x: hidden;
  position: relative;
  z-index: 1;
}

.sidebar_menu_item {
  padding: 8px 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 12px 8px;
  color: #717680;
  border-radius: 5px;
  cursor: pointer;
}

.sidebar_menu_item:hover {
  background-color: #0000001a;
}

.sidebar_menu_item.active {
  background-color: #f5faff;
  color: #1570ef;
}

.sidebar_expanded .sidebar_menu_item {
  padding: 8px 12px;
  justify-content: flex-start;
}

.sidebar_menu_item.active .menu_text {
  color: #1570ef;
}

.sidebar_menu_icon {
  width: 20px;
  height: 20px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.sidebar_menu_icon svg {
  width: 100%;
  height: 100%;
}

.sidebar_menu_misa_lomas_icon {
  background-image: url('@/assets/icons/misa-lomas.png');
  width: 20px;
  height: 20px;
  background-size: contain;
}

.sidebar_menu_loan_icon {
  background-image: url('@/assets/icons/lending.png');
  width: 20px;
  height: 20px;
  background-size: contain;
}

.sidebar_menu_accounting_service_icon {
  background-image: url('@/assets/icons/asp.png');
  width: 20px;
  height: 20px;
  background-size: contain;
}

.sidebar_menu_product_icon {
  background-image: url('@/assets/icons/cukcuk-no-bg.png');
  width: 20px;
  height: 20px;
  background-size: contain;
}

.menu_text {
  display: none;
  color: #101828;
  font-weight: 500;
  font-size: 13px;
  white-space: nowrap;
  flex-shrink: 0;
  padding-left: 8px;
}

.sidebar_expanded .menu_text {
  display: block;
}

.sidebar_toggle_text {
  color: inherit;
  white-space: nowrap;
  font-size: 14px;
  animation: fadeIn 0.3s ease;
}

.sidebar_menu_arrow {
  display: none;
  margin-left: auto;
}

.sidebar_expanded .sidebar_menu_arrow {
  display: block;
}

.sidebar_menu_divider {
  height: 1px;
  background-color: #e9eaeb;
  margin: 8px 12px;
}

.sidebar_toggle {
  /* Button component handles most styles. We just need to position it. */
  margin: 0 16px;
  flex-shrink: 0;
  z-index: 1;
}

.sidebar_expanded .sidebar_toggle {
  justify-content: center;
}

.sidebar_toggle:hover {
  background-color: #f5faff;
}

/*end of style sidebar*/
</style>

<script setup>
import { ref, watch, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import Button from '@/components/controls/buttons/Button.vue'
import ArrowDownIcon from '@/components/icons/ArrowDownIcon.vue'

const props = defineProps({
  isExpanded: Boolean,
})

const emit = defineEmits(['toggle'])
const router = useRouter()
const route = useRoute()

onMounted(() => {
  const savedState = localStorage.getItem('sidebarExpanded')
  if (savedState !== null) {
    const isExpanded = savedState === 'true'
    if (isExpanded !== props.isExpanded) {
      emit('toggle')
    }
  }
})

watch(
  () => props.isExpanded,
  (newValue) => {
    localStorage.setItem('sidebarExpanded', String(newValue))
  },
)
</script>
