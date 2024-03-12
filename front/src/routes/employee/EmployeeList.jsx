import axios from "axios";
import { useEffect, useState } from "react";
import { CompactTable } from "@table-library/react-table-library/compact";
import { useTheme } from "@table-library/react-table-library/theme";
import { getTheme } from "@table-library/react-table-library/baseline";
import { useNavigate } from "react-router-dom";
import { useSort } from "@table-library/react-table-library/sort";

const EmployeeList = () => {
  const [employees, setEmployees] = useState([]);
  const [searchValue, setSearchValue] = useState("");
  const [sorting, setSorting] = useState({});

  const navigate = useNavigate();
  const getEmployees = async () => {
    const queryParams = new URLSearchParams();
    if (searchValue) {
      queryParams.append("seachValue", searchValue);
    }
    if (sorting.sortKey) {
      queryParams.append("sortOrder", sorting.reverse ? "desc" : "asc");
      queryParams.append("sortField", sorting.sortKey);
    }
    const response = await axios.get(
      `http://213.171.5.147:756/api/Employee?${queryParams}`
    );
    setEmployees(response.data);
  };
  const data = {
    nodes: employees,
  };
  const deleteEmployee = async (id) => {
    await axios.delete(`http://213.171.5.147:756/api/Employee/${id}`);
    await getEmployees();
  };

  useEffect(() => {
    getEmployees();
  }, [sorting, searchValue]);

  const sort = useSort(
    data,
    {
      onChange: onSortChange,
    },
    {
      isServer: true      
    }
  );

  function onSortChange(action, state) {
    setSorting(state);
  }
  const theme = useTheme(getTheme());
  const columns = [
    { label: "Код", sort: { sortKey: "id" }, renderCell: (item) => item.id },
    {
      label: "ФИО",
      sort: { sortKey: "fullname" },
      renderCell: (item) => (
        <span
          style={{ cursor: "pointer" }}
          onClick={() => navigate(`/employee/${item.id}`)}
        >
          {item.fullName}
        </span>
      ),
    },
    {
      label: "Табельный номер",
      sort: { sortKey: "personalNumber" },
      renderCell: (item) => item.personalNumber,
    },
    {
      label: "Подразделение",
      sort: { sortKey: "department" },
      renderCell: (item) => item.department,
    },
    {
      label: "Действия",
      renderCell: (item) => (
        <div>
          <button onClick={() => navigate(`/promotion/0`, {state: {employeeId: item.id, employeeName: item.fullName}})}>Повысить зп</button>
          <button onClick={() => deleteEmployee(item.id)}>Удалить</button>
        </div>
      ),
    },
  ];

  return (
    <div>
      <div className="form-property list-header">
        <div className="form-property">
          <label>Поиск</label>
          <input
            type="text"
            value={searchValue}
            onChange={(e) => setSearchValue(e.target.value)}
          />
        </div>
        <button onClick={() => navigate("/employee/new")}>Добавить нового сотрудника</button>
      </div>
      <CompactTable columns={columns} data={data} theme={theme} sort={sort} />
    </div>
  );
};

export default EmployeeList;
