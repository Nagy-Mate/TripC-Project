import { useEffect, useState } from "react";
import type { Trip } from "../types/Trip";
import type { Destination } from "../types/Destination";
import apiClient from "../api/ApiClient";

function CreatePage() {
  const today = new Date().toISOString().split("T")[0];
  const [destinations, setDestinations] = useState<Array<Destination>>([]);

  const [newTrip, setNewTrip] = useState<Trip>({
    id: 0,
    name: "",
    startDate: new Date(),
    endDate: new Date(),
    destinationId: 0,
  });

  useEffect(() => {
    apiClient
      .get("/destinations")
      .then((res) => setDestinations(res.data))
      .catch((e) => console.error(e));
  }, []);

  const saveBtnOnClick = async () => {
    const startDate = new Date(newTrip.startDate);
    const endDate = new Date(newTrip.endDate);
    if (startDate > endDate) {
      [newTrip.startDate, newTrip.endDate] = [endDate, startDate];
    }

    if (newTrip.destinationId === 0 && newTrip.name.trim() === "") {
    } else {
      apiClient
        .post(`/trips`, newTrip)
        .then((res) => {
          switch (res.status) {
            case 201:
              console.log("User created successfully");
              break;
            case 400:
              console.error("Bad request");
              break;
            default:
              console.error("An error occurred");
          }
        })
        .catch((e) => console.error(e));
    }
  };

  return (
    <>
      <div>
        <h2>Create Trip</h2>
        <p>
          Name:{" "}
          <input
            type="text"
            value={newTrip.name || ""}
            onChange={(e) => setNewTrip({ ...newTrip, name: e.target.value })}
          />
        </p>
        <p>
          Start Date:{" "}
          <input
            type="date"
            value={
              newTrip.startDate
                ? new Date(newTrip.startDate).toISOString().split("T")[0]
                : ""
            }
            min={today}
            onChange={(e) => {
              const val = e.target.value;
              if (val === "") {
                setNewTrip({ ...newTrip, startDate: new Date() });
              } else {
                setNewTrip({ ...newTrip, startDate: new Date(val) });
              }
            }}
          />{" "}
        </p>

        <p>
          End Date:{" "}
          <input
            type="date"
            value={
              newTrip.endDate
                ? new Date(newTrip.endDate).toISOString().split("T")[0]
                : ""
            }
            min={today}
            onChange={(e) => {
              const val = e.target.value;
              if (val === "") {
                setNewTrip({ ...newTrip, endDate: new Date() });
              } else {
                setNewTrip({ ...newTrip, endDate: new Date(val) });
              }
            }}
          />
        </p>

        <p>
          Destinations:{" "}
          <select
            value={newTrip.destinationId || 0}
            onChange={(e) =>
              setNewTrip({
                ...newTrip,
                destinationId: Number(e.target.value),
              })
            }
          >
            <option value={0}>Select one...</option>
            {destinations.map((d) => (
              <option key={d.id} value={d.id}>
                {d.name}
              </option>
            ))}
          </select>
        </p>
        <button onClick={saveBtnOnClick} id="saveBtn">
          Save
        </button>
      </div>
    </>
  );
}

export default CreatePage;
