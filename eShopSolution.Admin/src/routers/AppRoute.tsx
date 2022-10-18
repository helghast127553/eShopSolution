import { FC } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { PageURL } from "../models/enum";
import Login from "../app/login";
import PublicRoute from "./PublicRoute";
import PrivateRoute from "./PrivateRoute";
import Product from "../app/product";
import Category from "../app/category";
import User from "../app/user";

interface Props {}

const AppRoute: FC<Props> = (props: Props) => {
  return (
    <Router>
      <Routes>
        <Route
          element={
            <PublicRoute>
              <Login />
            </PublicRoute>
          }
          path={PageURL.ADMIN_LOGIN}
        />
        <Route
          element={
            <PrivateRoute>
              <Product />
            </PrivateRoute>
          }
          path={PageURL.ADMIN}
        />
        <Route
          element={
            <PrivateRoute>
              <Product />
            </PrivateRoute>
          }
          path={PageURL.ADMIN_PRODUCT}
        />
        <Route
          element={
            <PrivateRoute>
              <Category />
            </PrivateRoute>
          }
          path={PageURL.ADMIN_CATEGORY}
        />
        <Route
          element={
            <PrivateRoute>
              <User />
            </PrivateRoute>
          }
          path={PageURL.ADMIN_USER}
        />
      </Routes>
    </Router>
  );
};

export default AppRoute;
