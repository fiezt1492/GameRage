import * as React from "react";
import { Button } from "@mui/material";
import { styled } from "@mui/material/styles";
import "./champion.css";
// eslint-disable-next-line
import { getChampName, mixChampions } from "../utils/index";
import { useGame } from "../pages/Game";

const BootstrapButton = styled(Button)({
  padding: "1px 0px",
  height: "63px",
  borderRadius: 0,
  transition: "0.5s",
  "&:hover": {
    img: {
      opacity: "0.5",
    },
  },
});

const Champion = ({ value, onClick, selected = false }) => {
  const { fromChamps } = useGame();
  const from = fromChamps || require("../utils/champions.json");
  return (
    <BootstrapButton
      style={selected ? { border: "2px solid #D14343" } : null}
      onClick={onClick}
      alt=""
      disabled={value === -1}
    >
      <img
        src={
          value === -1 ? "" : `/static/images/${getChampName(value, from)}.png`
        }
        width={60}
        height={60}
        style={
          value === -1
            ? { display: "none" }
            : selected
            ? { border: "6px solid #FFB020" }
            : null
        }
        alt={value === -1 ? "barrier" : `${getChampName(value, from)}`}
      />
    </BootstrapButton>
  );
};

export default Champion;
