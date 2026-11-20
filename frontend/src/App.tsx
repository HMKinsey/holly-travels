import { BrowserRouter, Route, Routes } from "react-router";
import "./App.css";
import MainLayout from "./layout/MainLayout";
import AdventurePage from "./pages/AdventurePage";
import AboutPage from "./pages/AboutPage";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<MainLayout />}>
          <Route path="/" element={<AdventurePage />} />
          <Route path="/about" element={<AboutPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
