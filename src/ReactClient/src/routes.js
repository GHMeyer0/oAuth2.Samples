import React, { Suspense, Fragment, lazy } from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import AuthGuard from "./components/AuthGuard";
import GuestGuard from "./components/GuestGuard";
import { Typography, Button } from "@material-ui/core";
import Login from "./pages/Login";
import Unprotected from "./pages/Unprotected";
import Protected from "./pages/Protected";

export const renderRoutes = (routes = []) => (
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
);

const routes = [
  {
    exact: true,
    path: "/404",
    component: <Typography>404</Typography>,
  },
  {
    exact: true,
    guard: GuestGuard,
    path: "/login",
    component: Login,
  },
  {
    exact: true,
    guard: AuthGuard,
    path: "/protected",
    component: Protected,
  },
  {
    exact: true,
    path: "/unprotected",
    component: Unprotected,
  },
];

export default routes;
