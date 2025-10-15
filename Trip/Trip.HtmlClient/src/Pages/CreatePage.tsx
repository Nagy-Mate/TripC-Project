import { useEffect, useState } from "react";
import type { Trip } from "../types/Trip";
import type { Destination } from "../types/Destination";
import apiClient from "../api/ApiClient";
import { Link } from "react-router";
import "../styles/App.css";

function CreatePage() {
  const today = new Date().toISOString().split("T")[0];
  const [destinations, setDestinations] = useState<Array<Destination>>([]);

  const [isTErrVisible, setIsTErrVisible] = useState<boolean>(false);
  const [isTErr2Visible, setIsTErr2Visible] = useState<boolean>(false);
  const [isTSuccVisible, SetIsTSuccVisible] = useState<boolean>(false);

  const [isDErrVisible, setIsDErrVisible] = useState<boolean>(false);
  const [isDErr2Visible, setIsDErr2Visible] = useState<boolean>(false);
  const [isDSuccVisible, SetIsDSuccVisible] = useState<boolean>(false);

  const [newTrip, setNewTrip] = useState<Trip>({
    id: 0,
    name: "",
    startDate: new Date(),
    endDate: new Date(),
    destinationId: 0,
  });

  const [newDest, setNewDest] = useState<Destination>({
    id: 0,
    name: "",
    country: "",
    description: "",
  });

  useEffect(() => {
    apiClient
      .get("/destinations")
      .then((res) => setDestinations(res.data))
      .catch((e) => console.error(e));
  }, [isDSuccVisible]);

  useEffect(() => {
    setIsTErrVisible(false);
    setIsTErr2Visible(false);
  }, [newTrip]);

  useEffect(() => {
    setIsDErrVisible(false);
    setIsDErr2Visible(false);
  }, [newDest]);

  const saveTripBtnOnClick = async () => {
    const startDate = new Date(newTrip.startDate);
    const endDate = new Date(newTrip.endDate);
    if (startDate > endDate) {
      [newTrip.startDate, newTrip.endDate] = [endDate, startDate];
    }

    if (newTrip.destinationId === 0 || newTrip.name.trim() === "") {
      setIsTErrVisible(true);
    } else {
      apiClient
        .post(`/trips`, newTrip)
        .then((res) => {
          switch (res.status) {
            case 201:
              console.log("User created successfully");

              setNewTrip({
                id: 0,
                name: "",
                startDate: new Date(),
                endDate: new Date(),
                destinationId: 0,
              });

              SetIsTSuccVisible(true);
              break;
            default:
              console.error("An error occurred");
              setIsTErr2Visible(true);
          }
        })
        .catch((e) => console.error(e));
    }
  };

  const saveDestBtnOnClick = async () => {
    if (
      newDest.name.trim() === "" ||
      newDest.country.trim() === "" ||
      newDest.description?.trim() === ""
    ) {
      setIsDErrVisible(true);
    } else {
      apiClient.post(`/destinations`, newDest).then((res) => {
        switch (res.status) {
          case 201:
            console.log("User created successfully");

            setNewDest({
              id: 0,
              name: "",
              country: "",
              description: "",
            });

            SetIsDSuccVisible(true);
            break;
          default:
            console.error("An error occurred");
            setIsDErr2Visible(true);
        }
      });
    }
  };

  return (
    <>
      <Link to="/">
        <button id="BackToButton">Home Page</button>
      </Link>
      <div style={{ display: "flex" }}>
        <div style={{ flex: 1, width: "400px" }}>
          <h2>Create Trip</h2>
          <p>
            Name:{" "}
            <input
              type="text"
              value={newTrip.name || ""}
              onChange={(e) => {
                setNewTrip({ ...newTrip, name: e.target.value });
                SetIsTSuccVisible(false);
              }}
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
                SetIsTSuccVisible(false);
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
                SetIsTSuccVisible(false);
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
              onChange={(e) => {
                setNewTrip({
                  ...newTrip,
                  destinationId: Number(e.target.value),
                });
                SetIsTSuccVisible(false);
              }}
            >
              <option value={0}>Select one...</option>
              {destinations.map((d) => (
                <option key={d.id} value={d.id}>
                  {d.name}
                </option>
              ))}
            </select>
          </p>
          <button onClick={saveTripBtnOnClick} id="saveBtn">
            Save
          </button>
          {isTErrVisible && <p className="inputErr">Invalid input</p>}

          {isTSuccVisible && <p className="inputSucc">Saved</p>}

          {isTErr2Visible && <p className="inputErr">An error occurred</p>}
        </div>
        <div style={{ flex: 1 }}>
          <h2>Create Destination</h2>
          <p>
            Name:{" "}
            <input
              type="text"
              value={newDest.name || ""}
              onChange={(e) => {
                setNewDest({ ...newDest, name: e.target.value });
                SetIsDSuccVisible(false);
              }}
            />
          </p>
          <p>
            Country:{" "}
            <input
              type="text"
              value={newDest.country || ""}
              onChange={(e) => {
                setNewDest({ ...newDest, country: e.target.value });
                SetIsDSuccVisible(false);
              }}
            />
          </p>
          <p>
            Description:{" "}
            <input
              type="text"
              value={newDest.description || ""}
              onChange={(e) => {
                setNewDest({ ...newDest, description: e.target.value });
                SetIsDSuccVisible(false);
              }}
            />
          </p>
          <button onClick={saveDestBtnOnClick} id="saveBtn">
            Save
          </button>
          {isDErrVisible && <p className="inputErr">Invalid input</p>}

          {isDSuccVisible && <p className="inputSucc">Saved</p>}

          {isDErr2Visible && <p className="inputErr">An error occurred</p>}
        </div>
      </div>
    </>
  );
}

export default CreatePage;
