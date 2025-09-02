# QorusDocs Customer Data Listing Application

## Application Overview

The **QorusDocs Customer Data Listing Application** is a modern, full-stack web application designed to manage and visualize customer information efficiently. It provides a seamless interface for viewing, searching, filtering, sorting, and paginating customer data.

## Features

### Search & Filter

* **Real-time Text Search:** Quickly find customers by **name** or **email**.
* **Status Filtering:** Easily filter customers based on **active** or **inactive** status.
* **Date Range Filtering:** Narrow down results based on **customer creation dates**.

### Sorting & Pagination

* **Column Sorting:** Sort customer information by **name, email, creation date, and country**.
* **Sort Direction Indicators:** Clear visual cues for ascending or descending order.
* **Efficient Pagination:** Handles large datasets seamlessly with server-side pagination.
* **Page Size Options:** Users can select the number of items displayed per page.
* **Total Count Display:** Always shows the total number of results matching the current filters.

### User Experience

* **Responsive and Modern UI:** Designed for a smooth and intuitive interaction.
* **Loading States:** Visual feedback while data is being fetched.
* **Error Handling:** Displays meaningful messages when issues occur.
* **Performance Optimized:** Ensures fast loading and responsive operations even with large datasets.

## Configuration

The application uses `appsettings.json` for configuration, including API endpoints and other environment-specific settings. **Do not commit sensitive values** such as production endpoints or secrets. Instead:

1. Create a copy of `appsettings.json.example` and rename it to `appsettings.json`.
2. Replace `DefaultConnection` with real connection string provided.

For production or CI/CD deployments, you can override settings with **environment variables**.
