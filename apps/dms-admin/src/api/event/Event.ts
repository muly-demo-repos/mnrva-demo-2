import { Device } from "../device/Device";
import { Decimal } from "decimal.js";

export type Event = {
  createdAt: Date;
  device?: Device | null;
  eventType: string | null;
  humidity: Decimal | null;
  id: string;
  temperature: Decimal | null;
  timestamp: Date | null;
  updatedAt: Date;
};
