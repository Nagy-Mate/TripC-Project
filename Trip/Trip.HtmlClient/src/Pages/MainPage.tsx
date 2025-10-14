import { useEffect, useState } from "react";
import "../styles/App.css";
import type { Trip } from "../types/Trip";
import apiClient from "../api/ApiClient";
import type { Destination } from "../types/Destination";
import { Link } from "react-router";

function MainPage() {
  const [trips, setTrips] = useState<Array<Trip>>([]);
  const [destinations, setDestinations] = useState<Array<Destination>>([]);
  const [tripToDeleteId, setTripToDeleteId] = useState<number | null>(null);
  const [filterDestId, setFilterDestId] = useState<number>(0);

  useEffect(() => {
    if (filterDestId === 0) {
      apiClient
        .get("/trips")
        .then((res) => setTrips(res.data))
        .catch((e) => console.error(e));
    } else if (destinations.find((d) => d.id == filterDestId)) {
      apiClient
        .get(`/trips/byDestination/${filterDestId}`)
        .then((res) => setTrips(res.data))
        .catch((e) => console.error(e));
    }
  }, [tripToDeleteId, filterDestId]);

  useEffect(() => {
    apiClient
      .get("/destinations")
      .then((res) => setDestinations(res.data))
      .catch((e) => console.error(e));
  }, []);

  useEffect(() => {
    if (tripToDeleteId != null) {
      apiClient
        .delete(`/trips/${tripToDeleteId}`)
        .catch((e) => console.error(e))
        .finally(() => setTripToDeleteId(null));
    }
  }, [tripToDeleteId]);
  return (
    <>
      <Link to="/create">
        <button id="createBtn">Create</button>
      </Link>
      <p>
        <b>Filter By Destinations: </b>
        <select
          value={filterDestId || 0}
          onChange={(e) => {
            setFilterDestId(Number(e.target.value));
          }}
        >
          <option value={0}>All</option>
          {destinations.map((d) => (
            <option key={d.id} value={d.id}>
              {d.name}
            </option>
          ))}
        </select>
      </p>
      {trips.length != 0 ? (
        <div>
          <table>
            <tbody>
              <tr>
                <th id="fth">Id</th>
                <th>Name</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th>Destination</th>
                <th>Actions</th>
              </tr>
              {trips.map((t) => (
                <tr key={t.id}>
                  <td>{t.id}</td>
                  <td>{t.name}</td>
                  <td>{new Date(t.startDate).toLocaleDateString()}</td>
                  <td>{new Date(t.endDate).toLocaleDateString()}</td>
                  <td>
                    {destinations.find((d) => d.id == t.destinationId)?.name}
                  </td>
                  <td>
                    <button
                      className="dButton"
                      onClick={() => setTripToDeleteId(t.id)}
                    >
                      Delete
                    </button>
                    <Link to={`/updateTirp/${t.id}`}>
                      <button className="uButton">Update</button>
                    </Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <div className="inputErr"> <h2>No trip</h2> </div>
      )}
    </>
  );
}

export default MainPage;
