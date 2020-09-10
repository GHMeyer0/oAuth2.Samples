import Container from "@material-ui/core/Container";
import { KeycloakProvider } from "@react-keycloak/web";
import axios from "axios";
import { createBrowserHistory } from "history";
import React, { useState } from "react";
import { MemoryRouter, Router } from "react-router-dom";
import keycloak from "./keycloak";
import routes, { renderRoutes } from "./routes";

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
