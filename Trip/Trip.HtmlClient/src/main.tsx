import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { BrowserRouter, Route, Routes } from "react-router";
import MainPage from "./Pages/MainPage";
import UpdatePage from "./Pages/UpdatePage";
import CreatePage from "./Pages/CreatePage";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainPage />} />
        <Route path="/updateTirp/:id" element={<UpdatePage />} />
        <Route path="/create" element={<CreatePage />} />
        <Route path="*" element={<h1>404 Page not found</h1>} />
      </Routes>
    </BrowserRouter>
  </StrictMode>
);
