import axios from "axios";
import { useEffect, useState } from "react";
import ReactDatePicker from "react-datepicker";
import { Controller, useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";
import "react-datepicker/dist/react-datepicker.css";

const Employee = () => {
  const { register, handleSubmit, reset, control } = useForm();
  const navigate = useNavigate();

  const { id } = useParams();
  const [educationOptions, setEducationOptions] = useState([]);
  const [departmentOptions, setDepartmentOptions] = useState([]);
  const getEducationOptions = async () => {
    var response = await axios.get(
      "http://213.171.5.147:756/api/Education"
    );
    setEducationOptions(response.data);
  };
  const getDepartmentOptions = async () => {
    var response = await axios.get(
      "http://213.171.5.147:756/api/Department"
    );
    setDepartmentOptions(response.data);
  };
  const getEmployee = async () => {
    var response = await axios.get(
      `http://213.171.5.147:756/api/Employee/${id}`
    );
    reset(response.data);
  };

  const save = async (data) => {
    const intId = Number.parseInt(id);
    if (intId) {
      await axios.put(
        `http://213.171.5.147:756/api/Employee/${intId}`,
        data
      );
    } else {
      await axios.post(
        "http://213.171.5.147:756/api/Employee",
        data
      );
    }
    navigate("/employee");
  };
  useEffect(() => {
    const intId = Number.parseInt(id);
    if (intId) {
      getEmployee();
    }
  }, [id]);

  useEffect(() => {
    getEducationOptions();
    getDepartmentOptions();
  }, []);

  return (
    <form onSubmit={handleSubmit((data) => save(data))}>
      <div className="form-property">
        <label>ФИО</label>
        <input {...register("fullName")} />
      </div>
      <div className="form-property">
        <label>Табельный номер</label>
        <input {...register("personalNumber")} />
      </div>
      <div className="form-property">
        <label>Пол</label>
        <select
          {...register("sex", {
            required: true,
            setValueAs: (value) => parseInt(value, 10),
          })}
        >
          <option value={0}>Мужской</option>
          <option value={1}>Женский</option>
        </select>
      </div>
      <div className="form-property">
        <label>Дата рождения</label>
        <Controller
          control={control}
          name="dateOfBirth"
          render={({ field }) => (
            <ReactDatePicker
              placeholderText="Select date"
              onChange={(date) => field.onChange(date)}
              selected={field.value}
            />
          )}
        />
      </div>
      <div className="form-property">
        <label>Департамент</label>
        <select
          {...register("departmentId", {
            required: true,
            setValueAs: (value) => parseInt(value, 10),
          })}
        >
          <option value={0}>Выберите значение</option>
          {departmentOptions.map((i) => (
            <option value={i.id}>{i.name}</option>
          ))}
        </select>
      </div>
      <div className="form-property">
        <label>Образование</label>
        <select
          {...register("educationId", {
            required: true,
            setValueAs: (value) => {
              return parseInt(value, 10);
            },
          })}
        >
          <option value={0}>Выберите значение</option>
          {educationOptions.map((i) => (
            <option value={i.id}>{i.name}</option>
          ))}
        </select>
      </div>
      <div className="form-property">
        <label>Дата принятия</label>
        <Controller
          control={control}
          name="dateOfEmployment"
          render={({ field }) => (
            <ReactDatePicker
              placeholderText="Select date"
              onChange={(date) => field.onChange(date)}
              selected={field.value}
            />
          )}
        />
      </div>
      <div className="form-property">
        <label>Дата увольнения</label>
        <Controller
          control={control}
          name="dateOfLeave"
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

export default Employee;
