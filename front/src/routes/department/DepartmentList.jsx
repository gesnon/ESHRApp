import axios from "axios";
import { useEffect, useState } from "react";
import { CompactTable } from "@table-library/react-table-library/compact";
import { useTheme } from "@table-library/react-table-library/theme";
import { getTheme } from "@table-library/react-table-library/baseline";
import { useNavigate } from "react-router-dom";

const DepartmentList = () => {
  const [department, setDepartment] = useState([]);
  const navigate = useNavigate();
  const getDepartment = async () => {
    const response = await axios.get("http://213.171.5.147:756/api/Department");
    setDepartment(response.data);
  };
  const deleteDepartment = async (id) => {
    await axios.delete(`http://213.171.5.147:756/api/Department/${id}`);
    await getDepartment();
  };
  useEffect(() => {
    getDepartment();
  }, []);

  const theme = useTheme(getTheme());
  const columns = [
    { label: "Код", renderCell: (item) => item.id },
    {
      label: "Название",
      renderCell: (item) => (
        <span
          style={{ cursor: "pointer" }}
          onClick={() => navigate(`/department/${item.id}`)}
        >
          {item.name}
        </span>
      ),
    },
    {
      label: "Действия",
      renderCell: (item) => (
        <div>
          <button onClick={() => deleteDepartment(item.id)}>Удалить</button>          
        </div>
      ),
    },
  ];

  const data = {
    nodes: department,
  };
  return (
    <div>
      <button onClick={() => navigate("/department/new")}>Добавить</button>
      <CompactTable columns={columns} data={data} theme={theme} />
    </div>
  );
};

export default DepartmentList;
