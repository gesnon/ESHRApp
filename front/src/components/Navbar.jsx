import React from "react";
import { Link, NavLink } from "react-router-dom";
import "../App.css";

function Navbar() {
  return (
    <>
      <nav id="sidebar" className="nav-menu active">
        <ul className="nav-menu-items">
          <li key={1} className="nav-text">
            <NavLink to="/employee">
              <span>Список сотрудников</span>
            </NavLink>
          </li>
          <li key={2} className="nav-text">
            <NavLink to="/promotion">
              <span>Список сотрудников на повышение</span>
            </NavLink>
          </li> 
          <li key={3} className="nav-text">
            <NavLink to="/education">
              <span>Образование</span>
            </NavLink>
          </li>
          <li key={4} className="nav-text">
            <NavLink to="/department">
              <span>Подразделение</span>
            </NavLink>
          </li>         
        </ul>
      </nav>
    </>
  );
}

export default Navbar;
