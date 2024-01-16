import { DeviceWhereUniqueInput } from "../device/DeviceWhereUniqueInput";
import { StringNullableFilter } from "../../util/StringNullableFilter";
import { DecimalNullableFilter } from "../../util/DecimalNullableFilter";
import { StringFilter } from "../../util/StringFilter";
import { DateTimeNullableFilter } from "../../util/DateTimeNullableFilter";

export type EventWhereInput = {
  device?: DeviceWhereUniqueInput;
  eventType?: StringNullableFilter;
  humidity?: DecimalNullableFilter;
  id?: StringFilter;
  temperature?: DecimalNullableFilter;
  timestamp?: DateTimeNullableFilter;
};
