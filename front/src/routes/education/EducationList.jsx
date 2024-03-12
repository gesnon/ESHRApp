import axios from "axios";
import { useEffect, useState } from "react";
import { CompactTable } from "@table-library/react-table-library/compact";
import { useTheme } from "@table-library/react-table-library/theme";
import { getTheme } from "@table-library/react-table-library/baseline";
import { useNavigate } from "react-router-dom";

const EducationList = () => {
  const [education, setEducation] = useState([]);
  const navigate = useNavigate();
  const getEducation = async () => {
    const response = await axios.get("http://213.171.5.147:756/api/Education");
    setEducation(response.data);
  };
  const deleteEducation = async (id) => {
    await axios.delete(`http://213.171.5.147:756/api/Education/${id}`);
    await getEducation();
  };
  useEffect(() => {
    getEducation();
  }, []);

  const theme = useTheme(getTheme());
  const columns = [
    { label: "Код", renderCell: (item) => item.id },
    {
      label: "Название",
      renderCell: (item) => (
        <span
          style={{ cursor: "pointer" }}
          onClick={() => navigate(`/education/${item.id}`)}
        >
          {item.name}
        </span>
      ),
    },
    {
      label: "Действия",
      renderCell: (item) => (
        <div>
          <button onClick={() => deleteEducation(item.id)}>Удалить</button>          
        </div>
      ),
    },
  ];

  const data = {
    nodes: education,
  };
  return (
    <div>
      <button onClick={() => navigate("/education/new")}>Добавить</button>
      <CompactTable columns={columns} data={data} theme={theme} />
    </div>
  );
};

export default EducationList;
