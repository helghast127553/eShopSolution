import react, { FC } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { PageURL } from "../models/enum";
import Login from "../app/login";
import PublicRoute from "./PublicRoute";
import PrivateRoute from "./PrivateRoute";
import Product from "../app/product/product";
import Category from "../app/category/category";
import User from "../app/user/user";

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
            <PublicRoute>
              <Product />
            </PublicRoute>
          }
          path={PageURL.ADMIN}
        />
        <Route
          element={
            <PublicRoute>
              <Product />
            </PublicRoute>
          }
          path={PageURL.ADMIN_PRODUCT}
        />
        <Route
          element={
            <PublicRoute>
              <Category />
            </PublicRoute>
          }
          path={PageURL.ADMIN_CATEGORY}
        />
        <Route
          element={
            <PublicRoute>
              <User />
            </PublicRoute>
          }
          path={PageURL.ADMIN_USER}
        />
      </Routes>
    </Router>
  );
};

export default AppRoute;
