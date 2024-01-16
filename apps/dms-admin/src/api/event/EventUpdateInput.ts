import { DeviceWhereUniqueInput } from "../device/DeviceWhereUniqueInput";
import { Decimal } from "decimal.js";

export type EventUpdateInput = {
  device?: DeviceWhereUniqueInput | null;
  eventType?: string | null;
  humidity?: Decimal | null;
  temperature?: Decimal | null;
  timestamp?: Date | null;
};
