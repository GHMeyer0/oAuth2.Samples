import React from "react";
import { Button } from "@material-ui/core";
import { useKeycloak } from "@react-keycloak/web";

const Login = () => {
  const { keycloak, initialized } = useKeycloak();
  return (
    <Button color="primary" onClick={keycloak.login}>
      Login
    </Button>
  );
};

export default Login;
