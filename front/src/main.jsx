import React from "react";
import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider, Outlet } from "react-router-dom";
import Navbar from "./components/Navbar";
import "./App.css";
import EmployeeList from "./routes/employee/EmployeeList";
import DepartmentList from "./routes/department/DepartmentList";
import Employee from "./routes/employee/Employee";
import EducationList from "./routes/education/EducationList";
import Education from "./routes/education/Education";
import Department from "./routes/department/Department";
import PromotionList from "./routes/promotion/PromotionList";
import Promotion from "./routes/promotion/Promotion";

const AppLayout = () => (
  <div className="page-body">
    <div className="page-sidebar">
      <Navbar />
    </div>
    <div className="page-content">
      <Outlet />
    </div>
  </div>
);

const router = createBrowserRouter([
  {
    element: <AppLayout />,
    children: [
      {
        path: "/",
        element: <EmployeeList />,
      },
      {
        path: "employee",
        element: <EmployeeList />,
      },
      {
        path: "employee/:id",
        element: <Employee />,
      },
      {
        path: "department",
        element: <DepartmentList />,
      },
      {
        path: "department/:id",
        element: <Department />,
      },
      {
        path: "education",
        element: <EducationList />,
      },
      {
        path: "education/:id",
        element: <Education />,
      },
      {
        path: "promotion",
        element: <PromotionList />,
      },
      {
        path: "promotion/:id",
        element: <Promotion />,
      },
    ],
  },
]);

createRoot(document.getElementById("root")).render(
  <RouterProvider router={router} />
);
