import React, { Fragment, lazy, Suspense } from "react";
import { Route, Switch } from "react-router-dom";
import AuthGuard from "./components/AuthGuard";
import GuestGuard from "./components/GuestGuard";
import LoadingScreen from "./components/LoadingScreen";

export const renderRoutes = (routes = []) => (
  <Suspense fallback={<LoadingScreen />}>
    <Switch>
      {routes.map((route, i) => {
        const Guard = route.guard || Fragment;
        const Layout = route.layout || Fragment;
        const Component = route.component;

        return (
          <Route
            key={i}
            path={route.path}
            exact={route.exact}
            render={(props) => (
              <Guard>
                <Layout>
                  {route.routes ? (
                    renderRoutes(route.routes)
                  ) : (
                    <Component {...props} />
                  )}
                </Layout>
              </Guard>
            )}
          />
        );
      })}
    </Switch>
  </Suspense>
);

const routes = [
  {
    exact: true,
    path: "/",
    component: lazy(() => import("./pages/Unprotected")),
  },
  {
    exact: true,
    guard: GuestGuard,
    path: "/login",
    component: lazy(() => import("./pages/Login")),
  },
  {
    exact: true,
    guard: AuthGuard,
    path: "/protected",
    component: lazy(() => import("./pages/Protected")),
  },
];

export default routes;
