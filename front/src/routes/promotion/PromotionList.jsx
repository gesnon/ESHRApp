import axios from "axios";
import { useEffect, useState } from "react";
import { CompactTable } from "@table-library/react-table-library/compact";
import { useTheme } from "@table-library/react-table-library/theme";
import { getTheme } from "@table-library/react-table-library/baseline";
import { useNavigate } from "react-router-dom";
import ReactDatePicker from "react-datepicker";
import { createPortal } from "react-dom";

const PromotionList = () => {
  const date = new Date();
  const [promotion, setPromotion] = useState([]);
  const [startDate, setStartDate] = useState(
    new Date(date.getFullYear(), date.getMonth(), 1)
  );
  const [endDate, setEndDate] = useState(
    new Date(date.getFullYear(), date.getMonth() + 1, 0)
  );
  const navigate = useNavigate();
  const getPromotion = async () => {
    const query = new URLSearchParams();
    query.append("startDate", startDate.toISOString());
    query.append("endDate", endDate.toISOString());
    const response = await axios.get(
      `http://213.171.5.147:756/api/Promotion?${query}`
    );
    setPromotion(response.data);
  };

  const downloadPromoReport = async () => {
    const query = new URLSearchParams();
    query.append("startDate", startDate.toISOString());
    query.append("endDate", endDate.toISOString());
    const response = await axios.get(
      `http://213.171.5.147:756/api/Promotion/report/promo?${query}`,
      { responseType: "blob" }
    );
    // create file link in browser's memory
    const href = URL.createObjectURL(response.data);

    // create "a" HTML element with href to file & click
    const link = document.createElement("a");
    link.href = href;
    link.setAttribute("download", "Отчет по сотрудникам с повышением зп.xlsx"); //or any other extension
    document.body.appendChild(link);
    link.click();

    // clean up "a" element & remove ObjectURL
    document.body.removeChild(link);
    URL.revokeObjectURL(href);
  };
  const downloadNoPromoReport = async () => {
    const query = new URLSearchParams();
    query.append("startDate", startDate.toISOString());
    query.append("endDate", endDate.toISOString());
    const response = await axios.get(
      `http://213.171.5.147:756/api/Promotion/report/no-promo?${query}`,
      { responseType: "blob" }
    );
    // create file link in browser's memory
    const href = URL.createObjectURL(response.data);

    // create "a" HTML element with href to file & click
    const link = document.createElement("a");
    link.href = href;
    link.setAttribute(
      "download",
      "Отчет по сотрудникам без повышением зп.xlsx"
    ); //or any other extension
    document.body.appendChild(link);
    link.click();

    // clean up "a" element & remove ObjectURL
    document.body.removeChild(link);
    URL.revokeObjectURL(href);
  };
  const deletePromotion = async (id) => {
    await axios.delete(`http://213.171.5.147:756/api/Promotion/${id}`);
    await getPromotion();
  };
  useEffect(() => {
    getPromotion();
  }, [startDate, endDate]);

  const theme = useTheme(getTheme());
  const columns = [
    {
      label: "Сотрудник",
      renderCell: (item) => (
        <span
          style={{ cursor: "pointer" }}
          onClick={() => navigate(`/employee/${item.employeeId}`)}
        >
          {item.employeeName}
        </span>
      ),
    },
    {
      label: "% повышения оклада",
      renderCell: (item) => <span>{item.increasePercentage}</span>,
    },
    {
      label: "Дата повышения",
      renderCell: (item) => (
        <span>{new Date(item.promotionDate).toLocaleDateString()}</span>
      ),
    },
    {
      label: "Действия",
      renderCell: (item) => (
        <div>
          <button onClick={() => navigate(`/promotion/${item.id}`)}>
            Редактировать
          </button>
          <button onClick={() => deletePromotion(item.id)}>Удалить</button>
        </div>
      ),
    },
  ];

  const data = {
    nodes: promotion,
  };
  return (
    <div>
      <div className="form-property promo-header">
        <div className="filters">
          <label>Начало</label>
          <ReactDatePicker
            popperContainer={({ children }) =>
              createPortal(children, document.body)
            }
            placeholderText="Select date"
            onChange={(date) => setStartDate(date)}
            selected={startDate}
          />
          <label>Конец</label>
          <ReactDatePicker
            popperContainer={({ children }) =>
              createPortal(children, document.body)
            }
            placeholderText="Select date"
            onChange={(date) => setEndDate(date)}
            selected={endDate}
          />
        </div>
        <div className="promo-buttons">
          <button onClick={() => downloadPromoReport()}>
            Отчет по сотрудникам на повышение зп
          </button>
          <button onClick={() => downloadNoPromoReport()}>
            Отчет по сотрудникам без повышения зп
          </button>
        </div>
      </div>

      <CompactTable columns={columns} data={data} theme={theme} />
    </div>
  );
};

export default PromotionList;
