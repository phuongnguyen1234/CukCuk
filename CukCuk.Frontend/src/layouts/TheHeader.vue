<script setup lang="ts">
import ArrowDownIcon from '@/components/icons/ArrowDownIcon.vue'
import Setting from '@/components/icons/SettingIcon.vue'
</script>

<template>
  <!--Nav bar-->
  <div class="navbar">
    <!--navbar left-->
    <div class="navbar_left">
      <!--apps-->
      <svg
        class="svg-icon"
        width="18"
        height="18"
        viewBox="0 0 18 18"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          d="M1 2C1 2.265 1.1054 2.52 1.2929 2.707C1.4804 2.895 1.7348 3 2 3C2.2652 3 2.5196 2.895 2.7071 2.707C2.8946 2.52 3 2.265 3 2C3 1.735 2.8946 1.48 2.7071 1.293C2.5196 1.105 2.2652 1 2 1C1.7348 1 1.4804 1.105 1.2929 1.293C1.1054 1.48 1 1.735 1 2Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M8 2C8 2.265 8.1054 2.52 8.2929 2.707C8.4804 2.895 8.7348 3 9 3C9.2652 3 9.5196 2.895 9.7071 2.707C9.8946 2.52 10 2.265 10 2C10 1.735 9.8946 1.48 9.7071 1.293C9.5196 1.105 9.2652 1 9 1C8.7348 1 8.4804 1.105 8.2929 1.293C8.1054 1.48 8 1.735 8 2Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M15 2C15 2.265 15.1054 2.52 15.2929 2.707C15.4804 2.895 15.7348 3 16 3C16.2652 3 16.5196 2.895 16.7071 2.707C16.8946 2.52 17 2.265 17 2C17 1.735 16.8946 1.48 16.7071 1.293C16.5196 1.105 16.2652 1 16 1C15.7348 1 15.4804 1.105 15.2929 1.293C15.1054 1.48 15 1.735 15 2Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M1 9C1 9.265 1.1054 9.52 1.2929 9.707C1.4804 9.895 1.7348 10 2 10C2.2652 10 2.5196 9.895 2.7071 9.707C2.8946 9.52 3 9.265 3 9C3 8.735 2.8946 8.48 2.7071 8.293C2.5196 8.105 2.2652 8 2 8C1.7348 8 1.4804 8.105 1.2929 8.293C1.1054 8.48 1 8.735 1 9Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M8 9C8 9.265 8.1054 9.52 8.2929 9.707C8.4804 9.895 8.7348 10 9 10C9.2652 10 9.5196 9.895 9.7071 9.707C9.8946 9.52 10 9.265 10 9C10 8.735 9.8946 8.48 9.7071 8.293C9.5196 8.105 9.2652 8 9 8C8.7348 8 8.4804 8.105 8.2929 8.293C8.1054 8.48 8 8.735 8 9Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M15 9C15 9.265 15.1054 9.52 15.2929 9.707C15.4804 9.895 15.7348 10 16 10C16.2652 10 16.5196 9.895 16.7071 9.707C16.8946 9.52 17 9.265 17 9C17 8.735 16.8946 8.48 16.7071 8.293C16.5196 8.105 16.2652 8 16 8C15.7348 8 15.4804 8.105 15.2929 8.293C15.1054 8.48 15 8.735 15 9Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M1 16C1 16.265 1.1054 16.52 1.2929 16.707C1.4804 16.895 1.7348 17 2 17C2.2652 17 2.5196 16.895 2.7071 16.707C2.8946 16.52 3 16.265 3 16C3 15.735 2.8946 15.48 2.7071 15.293C2.5196 15.105 2.2652 15 2 15C1.7348 15 1.4804 15.105 1.2929 15.293C1.1054 15.48 1 15.735 1 16Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M8 16C8 16.265 8.1054 16.52 8.2929 16.707C8.4804 16.895 8.7348 17 9 17C9.2652 17 9.5196 16.895 9.7071 16.707C9.8946 16.52 10 16.265 10 16C10 15.735 9.8946 15.48 9.7071 15.293C9.5196 15.105 9.2652 15 9 15C8.7348 15 8.4804 15.105 8.2929 15.293C8.1054 15.48 8 15.735 8 16Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
        <path
          d="M15 16C15 16.265 15.1054 16.52 15.2929 16.707C15.4804 16.895 15.7348 17 16 17C16.2652 17 16.5196 16.895 16.7071 16.707C16.8946 16.52 17 16.265 17 16C17 15.735 16.8946 15.48 16.7071 15.293C16.5196 15.105 16.2652 15 16 15C15.7348 15 15.4804 15.105 15.2929 15.293C15.1054 15.48 15 15.735 15 16Z"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        />
      </svg>

      <!--logo-->
      <div class="navbar_left_logo"></div>

      <!--title-->
      <div class="navbar_left_title">MISA CukCuk</div>

      <!--restaurant chooser-->
      <div class="navbar_left_restaurant_chooser">
        <div>Nhà hàng phở Phú Gia</div>
        <ArrowDownIcon color="#fff" size="16" />
      </div>
    </div>

    <!--navbar right-->
    <div class="navbar_right">
      <!--settings-->
      <div class="navbar_right_settings navbar_right_item">
        <Setting color="#fff" />
      </div>

      <!--chatbot-->
      <div class="navbar_right_chatbot navbar_right_item">
        <div class="navbar_right_chatbot_icon"></div>
      </div>

      <!--message-->
      <div class="navbar_right_message navbar_right_item">
        <div class="navbar_right_message_number">20</div>
      </div>

      <!--bell-->
      <div class="navbar_right_bell navbar_right_item">
        <svg
          class="svg-icon"
          width="18"
          height="20"
          viewBox="0 0 18 20"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M6.00012 15V16C6.00012 16.796 6.31615 17.559 6.87915 18.121C7.44115 18.684 8.20412 19 9.00012 19C9.79612 19 10.5591 18.684 11.1211 18.121C11.6841 17.559 12.0001 16.796 12.0001 16V15M7.00012 3C7.00012 2.47 7.21112 1.961 7.58612 1.586C7.96112 1.211 8.47012 1 9.00012 1C9.53012 1 10.0391 1.211 10.4141 1.586C10.7891 1.961 11.0001 2.47 11.0001 3C12.1481 3.543 13.1271 4.38801 13.8321 5.44501C14.5371 6.50201 14.9401 7.731 15.0001 9V12C15.0751 12.622 15.2951 13.217 15.6431 13.738C15.9901 14.259 16.4551 14.691 17.0001 15H1.00012C1.54512 14.691 2.01012 14.259 2.35712 13.738C2.70512 13.217 2.92512 12.622 3.00012 12V9C3.06012 7.731 3.46315 6.50201 4.16815 5.44501C4.87315 4.38801 5.85212 3.543 7.00012 3Z"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </div>

      <!--help-->
      <div class="navbar_right_help navbar_right_item">
        <svg
          width="20"
          height="20"
          viewBox="0 0 20 20"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
          class="svg-icon"
        >
          <path
            d="M9.75 14.75V14.76M9.75 11.25C9.73159 10.9254 9.81916 10.6036 9.99952 10.333C10.1799 10.0625 10.4433 9.85787 10.75 9.75001C11.1259 9.60627 11.4632 9.37725 11.7356 9.08097C12.0079 8.78469 12.2077 8.42923 12.3193 8.0426C12.4309 7.65597 12.4513 7.24871 12.3787 6.85288C12.3062 6.45705 12.1428 6.08346 11.9013 5.76153C11.6599 5.43959 11.347 5.1781 10.9873 4.99763C10.6276 4.81716 10.2309 4.72265 9.82853 4.72154C9.42611 4.72042 9.02892 4.81273 8.66824 4.9912C8.30756 5.16967 7.99323 5.42942 7.75 5.75001M0.75 9.75C0.75 10.9319 0.982792 12.1022 1.43508 13.1942C1.88738 14.2861 2.55031 15.2782 3.38604 16.114C4.22177 16.9497 5.21392 17.6126 6.30585 18.0649C7.39778 18.5172 8.5681 18.75 9.75 18.75C10.9319 18.75 12.1022 18.5172 13.1942 18.0649C14.2861 17.6126 15.2782 16.9497 16.114 16.114C16.9497 15.2782 17.6126 14.2861 18.0649 13.1942C18.5172 12.1022 18.75 10.9319 18.75 9.75C18.75 8.5681 18.5172 7.39778 18.0649 6.30585C17.6126 5.21392 16.9497 4.22177 16.114 3.38604C15.2782 2.55031 14.2861 1.88738 13.1942 1.43508C12.1022 0.982792 10.9319 0.75 9.75 0.75C8.5681 0.75 7.39778 0.982792 6.30585 1.43508C5.21392 1.88738 4.22177 2.55031 3.38604 3.38604C2.55031 4.22177 1.88738 5.21392 1.43508 6.30585C0.982792 7.39778 0.75 8.5681 0.75 9.75Z"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </div>

      <!--more-->
      <div class="navbar_right_more navbar_right_item">
        <svg
          width="20"
          height="20"
          viewBox="0 0 20 20"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
          class="svg-icon"
        >
          <path
            d="M5.75 9.75V9.76M9.75 9.75V9.76M13.75 9.75V9.76M0.75 9.75C0.75 10.9319 0.982792 12.1022 1.43508 13.1942C1.88738 14.2861 2.55031 15.2782 3.38604 16.114C4.22177 16.9497 5.21392 17.6126 6.30585 18.0649C7.39778 18.5172 8.5681 18.75 9.75 18.75C10.9319 18.75 12.1022 18.5172 13.1942 18.0649C14.2861 17.6126 15.2782 16.9497 16.114 16.114C16.9497 15.2782 17.6126 14.2861 18.0649 13.1942C18.5172 12.1022 18.75 10.9319 18.75 9.75C18.75 8.5681 18.5172 7.39778 18.0649 6.30585C17.6126 5.21392 16.9497 4.22177 16.114 3.38604C15.2782 2.55031 14.2861 1.88738 13.1942 1.43508C12.1022 0.982792 10.9319 0.75 9.75 0.75C8.5681 0.75 7.39778 0.982792 6.30585 1.43508C5.21392 1.88738 4.22177 2.55031 3.38604 3.38604C2.55031 4.22177 1.88738 5.21392 1.43508 6.30585C0.982792 7.39778 0.75 8.5681 0.75 9.75Z"
            stroke="currentColor"
            stroke-width="1.5"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </div>

      <!--profile-->
      <div class="navbar_right_profile navbar_right_item"></div>
      <div></div>
    </div>
  </div>
</template>

<style scoped>
/*style navbar*/
.navbar {
  display: flex;
  justify-content: space-between;
  position: absolute;
  top: 0;
  right: 0;
  left: 0;
  height: 48px;
  background-color: var(--color-primary);
  z-index: 100;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  padding: 0 16px 0 16px;
  border-bottom: 0.5px;
}

.navbar_left {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 12px;
}

.svg-icon {
  color: #fff;
}

.navbar_left_logo {
  background-image: url('@/assets/icons/CUKCUK.png');
  width: 32px;
  height: 32px;
  background-repeat: no-repeat;
  background-size: contain;
}

.navbar_left_title {
  font-weight: 600;
  font-size: 18px;
  color: #fff;
}

.navbar_left_restaurant_chooser {
  min-width: 30%;
  font-size: 14px;
  padding: 6px 12px;
  display: flex;
  flex-direction: row;
  align-items: center;
  transition:
    background-color 0.2s ease,
    box-shadow 0.2s ease;
  margin-left: 10px;
  color: #fff;
  gap: 16px;
  font-weight: 500;
  border-radius: 8px;
  cursor: pointer;
}

.navbar_left_restaurant_chooser:hover {
  background-color: rgba(0, 0, 0, 0.1);
}

.navbar_right {
  display: flex;
  align-items: center;
  height: 100%;
  gap: 8px;
}

.navbar_right_settings {
  display: flex;
  align-items: center;
  cursor: pointer;
  transition: color 0.2s ease;
}

.navbar_right_settings:hover {
  color: #2680eb;
}

.navbar_right_item {
  padding: 5px;
  border-radius: 100%;
  width: 27px;
  height: 27px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background-color 0.2s ease;
  background-position: center;
  background-repeat: no-repeat;
}

.navbar_right_item:hover {
  background-color: #0000001a;
}

.navbar_right_settings i {
  font-size: 22px;
  background-size: contain;
}

.navbar_right_chatbot_icon {
  background-image: url('@/assets/icons/AVA.png');
  width: 27px;
  height: 27px;
  background-repeat: no-repeat;
}

.navbar_right_bell_icon {
  background-image: url('https://cdnstore.misacdn.net/lib/static/cs-icon-store.svg');
  width: 20px;
  height: 20px;
}

.navbar_right_bell {
  position: relative;
}

.navbar_right_message_number {
  border-radius: 9px;
  background-color: red;
  color: white;
  text-align: center;
  font-size: 10px;
  position: absolute;
  top: 0;
  left: 17px;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 2px 5px;
}

.navbar_right_help {
  width: 20px;
  height: 20px;
}

.navbar_right_message {
  background-image: url('@/assets/icons/amis-chat.png');
  position: relative;
  width: 24px;
  height: 24px;
  background-repeat: no-repeat;
}

.navbar_right_help {
  width: 24px;
  height: 24px;
}

.navbar_right_more {
  width: 24px;
  height: 24px;
}

.navbar_right_tips {
  background-image: url(data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjQiIGhlaWdodD0iMjQiIHZpZXdCb3g9IjAgMCAyNCAyNCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4NCjxwYXRoIGQ9Ik0zIDEwVjIxIiBzdHJva2U9IiNGRkFBMkIiIHN0cm9rZS13aWR0aD0iMiIgc3Ryb2tlLWxpbmVjYXA9InJvdW5kIiBzdHJva2UtbGluZWpvaW49InJvdW5kIi8+DQo8cGF0aCBkPSJNMyAxMEMzLjA2NjY3IDkuNjY2NjcgMy41NiA5IDUgOSIgc3Ryb2tlPSIjRkZBQTJCIiBzdHJva2Utd2lkdGg9IjIiIHN0cm9rZS1saW5lY2FwPSJyb3VuZCIgc3Ryb2tlLWxpbmVqb2luPSJyb3VuZCIvPg0KPHBhdGggZD0iTTIxIDEwQzIwLjkzMzMgOS42NjY2NyAyMC40NCA5IDE5IDkiIHN0cm9rZT0iI0ZGQUEyQiIgc3Ryb2tlLXdpZHRoPSIyIiBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiLz4NCjxwYXRoIGQ9Ik0yMSAxMFYyMSIgc3Ryb2tlPSIjRkZBQTJCIiBzdHJva2Utd2lkdGg9IjIiIHN0cm9rZS1saW5lY2FwPSJyb3VuZCIgc3Ryb2tlLWxpbmVqb2luPSJyb3VuZCIvPg0KPHBhdGggZD0iTTMgMjFDNC4zNjgxNyAyMC4yMSA1LjkyMDE3IDE5Ljc5NDIgNy41IDE5Ljc5NDJDOS4wNzk4MyAxOS43OTQyIDEwLjYzMTggMjAuMjEgMTIgMjFDMTMuMzY4MiAyMC4yMSAxNC45MjAyIDE5Ljc5NDIgMTYuNSAxOS43OTQyQzE4LjA3OTggMTkuNzk0MiAxOS42MzE4IDIwLjIxIDIxIDIxIiBzdHJva2U9IiNGRkFBMkIiIHN0cm9rZS13aWR0aD0iMiIgc3Ryb2tlLWxpbmVjYXA9InJvdW5kIiBzdHJva2UtbGluZWpvaW49InJvdW5kIi8+DQo8cGF0aCBkPSJNOS42IDEyLjA3MTRDOC45MjgzOCAxMS41NzY3IDguNDMyMjcgMTAuODg3IDguMTgxOTUgMTAuMDk5OUM3LjkzMTYzIDkuMzEyOTEgNy45Mzk3OCA4LjQ2ODQ3IDguMjA1MjYgNy42ODYyNUM4LjQ3MDc1IDYuOTA0MDIgOC45ODAxIDYuMjIzNjYgOS42NjExNiA1Ljc0MTU1QzEwLjM0MjIgNS4yNTk0MyAxMS4xNjA1IDUgMTIgNUMxMi44Mzk1IDUgMTMuNjU3OCA1LjI1OTQzIDE0LjMzODggNS43NDE1NUMxNS4wMTk5IDYuMjIzNjYgMTUuNTI5MyA2LjkwNDAyIDE1Ljc5NDcgNy42ODYyNUMxNi4wNjAyIDguNDY4NDcgMTYuMDY4NCA5LjMxMjkxIDE1LjgxODEgMTAuMDk5OUMxNS41Njc3IDEwLjg4NyAxNS4wNzE2IDExLjU3NjcgMTQuNCAxMi4wNzE0QzE0LjA4NzcgMTIuMzc1MSAxMy44NTI1IDEyLjc0NjcgMTMuNzEzNSAxMy4xNTYyQzEzLjU3NDUgMTMuNTY1NyAxMy41MzU2IDE0LjAwMTYgMTMuNiAxNC40Mjg2QzEzLjYgMTQuODQ1MyAxMy40MzE0IDE1LjI0NSAxMy4xMzE0IDE1LjUzOTdDMTIuODMxMyAxNS44MzQ0IDEyLjQyNDMgMTYgMTIgMTZDMTEuNTc1NyAxNiAxMS4xNjg3IDE1LjgzNDQgMTAuODY4NiAxNS41Mzk3QzEwLjU2ODYgMTUuMjQ1IDEwLjQgMTQuODQ1MyAxMC40IDE0LjQyODZDMTAuNDY0NCAxNC4wMDE2IDEwLjQyNTUgMTMuNTY1NyAxMC4yODY1IDEzLjE1NjJDMTAuMTQ3NSAxMi43NDY3IDkuOTEyMzUgMTIuMzc1MSA5LjYgMTIuMDcxNFoiIHN0cm9rZT0iI0ZGQUEyQiIgc3Ryb2tlLXdpZHRoPSIyIiBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiLz4NCjxwYXRoIGQ9Ik0xMC4xNjAyIDEyLjg1NzJIMTMuODQwMiIgc3Ryb2tlPSIjRkZBQTJCIiBzdHJva2Utd2lkdGg9IjEuNSIgc3Ryb2tlLWxpbmVjYXA9InJvdW5kIiBzdHJva2UtbGluZWpvaW49InJvdW5kIi8+DQo8cGF0aCBkPSJNMTIgMkwxMiAxIiBzdHJva2U9IiNGRkFBMkIiIHN0cm9rZS13aWR0aD0iMiIgc3Ryb2tlLWxpbmVjYXA9InJvdW5kIiBzdHJva2UtbGluZWpvaW49InJvdW5kIi8+DQo8cGF0aCBkPSJNNyA0TDYuMDQ0OTkgMy43MDM0NCIgc3Ryb2tlPSIjRkZBQTJCIiBzdHJva2Utd2lkdGg9IjIiIHN0cm9rZS1saW5lY2FwPSJyb3VuZCIgc3Ryb2tlLWxpbmVqb2luPSJyb3VuZCIvPg0KPHBhdGggZD0iTTE2Ljk5OTggNEwxNy45NTQ4IDMuNzAzNDQiIHN0cm9rZT0iI0ZGQUEyQiIgc3Ryb2tlLXdpZHRoPSIyIiBzdHJva2UtbGluZWNhcD0icm91bmQiIHN0cm9rZS1saW5lam9pbj0icm91bmQiLz4NCjwvc3ZnPg0K);
  width: 24px;
  height: 24px;
  background-repeat: no-repeat;
}

.navbar_right_profile {
  background-image: url('@/assets/icons/avatar.png');
  width: 24px;
  height: 24px;
  border-radius: 50%;
  background-position: center;
  border: 1px solid #d5d7da;
  background-size: contain;
}

/*end of style navbar*/
</style>
