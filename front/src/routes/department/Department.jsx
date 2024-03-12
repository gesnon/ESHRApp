import axios from "axios";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";
import "react-datepicker/dist/react-datepicker.css";

const Department = () => {
  const { register, handleSubmit, reset } = useForm();
  const navigate = useNavigate();

  const { id } = useParams();
  const getDepartment = async () => {
    var response = await axios.get(
      `http://213.171.5.147:756/api/Department/${id}`
    );
    reset(response.data);
  };

  const save = async (data) => {
    const intId = Number.parseInt(id);
    if (intId) {
      await axios.put(`http://213.171.5.147:756/api/Department/${intId}`, data);
    } else {
      await axios.post("http://213.171.5.147:756/api/Department", data);
    }
    navigate("/department");
  };
  useEffect(() => {
    const intId = Number.parseInt(id);
    if (intId) {
      getDepartment();
    }
  }, [id]);

  return (
    <form onSubmit={handleSubmit((data) => save(data))}>
      <div className="form-property">
        <label>Название</label>
        <input {...register("name")} />
      </div>
      <button type="submit">Сохранить</button>
    </form>
  );
};

export default Department;
