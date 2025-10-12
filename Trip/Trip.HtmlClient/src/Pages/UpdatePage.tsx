import { useNavigate, useParams } from "react-router";
import type { Destination } from "../types/Destination";
import { useEffect, useState } from "react";
import apiClient from "../api/ApiClient";
import type { Trip } from "../types/Trip";

function UpdatePage() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [destinations, setDestinations] = useState<Array<Destination>>([]);
  const [trip, setTrip] = useState<Trip>();

  const today = new Date().toISOString().split("T")[0];

  const [newTrip, setNewTrip] = useState<Trip>({
    id: 0,
    name: "",
    startDate: new Date(),
    endDate: new Date(),
    destinationId: 0,
  });

  useEffect(() => {
    apiClient
      .get(`/trips/${id}`)
      .then((res) => {
        setTrip(res.data);
        setNewTrip(res.data);
      })
      .catch((e) => console.error(e));
  }, [id]);

  useEffect(() => {
    apiClient
      .get("/destinations")
      .then((res) => setDestinations(res.data))
      .catch((e) => console.error(e));
  }, []);

  const saveBtnOnClick = async () => {
    setNewTrip((prev) => ({
      ...prev,
      name: prev.name.trim() === "" ? trip?.name || "" : prev.name.trim(),
    }));

    apiClient
      .put(`/trips/${trip?.id}`, newTrip)
      .then((res) => {
        switch (res.status) {
          case 204:
            navigate("/");
            break;
          case 400:
            console.error("Bad request");
            break;
          default:
            console.error("An error occurred");
        }
      })
      .catch((e) => console.error(e));
  };

  return (
    <>
      {trip ? (
        <div>
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
              onChange={(e) =>
                setNewTrip({ ...newTrip, startDate: new Date(e.target.value) })
              }
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
              onChange={(e) =>
                setNewTrip({ ...newTrip, endDate: new Date(e.target.value) })
              }
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
          <button onClick={saveBtnOnClick}>Save</button>
        </div>
      ) : (
        <h1>Trip Not found</h1>
      )}
    </>
  );
}
export default UpdatePage;
