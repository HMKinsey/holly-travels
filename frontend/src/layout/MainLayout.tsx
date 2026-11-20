import { Outlet } from "react-router";

function MainLayout() {
  return (
    <main id="center">
      <Outlet />
    </main>
  );
}

export default MainLayout;
