import React, { useState } from "react";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Box from "@material-ui/core/Box";
import Link from "@material-ui/core/Link";
import { Button } from "@material-ui/core";
import axios from "axios";
import routes, { renderRoutes } from "./routes";
import { Router, MemoryRouter } from "react-router-dom";
import { createBrowserHistory } from "history";
import { KeycloakProvider } from "@react-keycloak/web";
import keycloak from "./keycloak";

const history = createBrowserHistory();

const App = () => {
  const getPermissions = () => {
    axios
      .post(
        "https://tuneauth.com.br/auth/realms/excelencia-dev/protocol/openid-connect/token",
        {
          params: {
            grant_type: "urn:ietf:params:oauth:grant-type:uma-ticket",
            audience: "exemple-api",
            response_include_resource_name: "true",
            response_mode: "permissions",
          },
        }
      )
      .then((res, err) => setstate(res));
  };

  const [state, setstate] = useState();

  return (
    <Container maxWidth="sm">
      <MemoryRouter initialEntries={["/"]} initialIndex={0}>
        <Router history={history}>
          <KeycloakProvider keycloak={keycloak}>
            {renderRoutes(routes)}
          </KeycloakProvider>
        </Router>
      </MemoryRouter>
    </Container>
  );
};

export default App;
