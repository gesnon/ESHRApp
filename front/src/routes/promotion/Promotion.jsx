import axios from "axios";
import { useEffect, useState } from "react";
import ReactDatePicker from "react-datepicker";
import { Controller, useForm } from "react-hook-form";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import "react-datepicker/dist/react-datepicker.css";

const Promotion = () => {
  const { register, handleSubmit, reset, control } = useForm();
  const navigate = useNavigate();
  const { state } = useLocation();
  const { id } = useParams();
  const getPromotion = async () => {
    var response = await axios.get(
      `http://213.171.5.147:756/api/Promotion/${id}`
    );
    reset(response.data);
  };

  const save = async (data) => {
    const intId = Number.parseInt(id);
    if (intId) {
      await axios.put(`http://213.171.5.147:756/api/Promotion/${intId}`, data);
    } else {
      await axios.post("http://213.171.5.147:756/api/Promotion", data);
    }
    navigate("/promotion");
  };
  useEffect(() => {
    const intId = Number.parseInt(id);
    if (intId) {
      getPromotion();
    }
    if (state !== null && state.employeeId) {
      reset({ employeeId: state.employeeId, employee: state.employeeName });
    }
  }, [id]);

  return (
    <form onSubmit={handleSubmit((data) => save(data))}>
      <div className="form-property">
        <label>Сотрудник</label>
        <input readOnly={true} {...register("employee")} />
      </div>
      <div className="form-property">
        <label>% повышения оклада</label>
        <input {...register("increasePercentage")} />
      </div>
      <div className="form-property">
        <label>Дата повышения</label>
        <Controller
          control={control}
          name="promotionDate"
          render={({ field }) => (
            <ReactDatePicker
              placeholderText="Select date"
              onChange={(date) => field.onChange(date)}
              selected={field.value}
            />
          )}
        />
      </div>
      <button type="submit">Сохранить</button>
    </form>
  );
};

export default Promotion;
