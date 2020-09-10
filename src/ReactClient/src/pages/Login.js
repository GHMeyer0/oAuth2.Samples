import { Button } from "@material-ui/core";
import { useKeycloak } from "@react-keycloak/web";
import React from "react";

const Login = () => {
  const { keycloak } = useKeycloak();
  return (
    <Button color="primary" onClick={keycloak.login}>
      Login
    </Button>
  );
};

export default Login;
