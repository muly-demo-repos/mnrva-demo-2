import { StringNullableFilter } from "../../util/StringNullableFilter";
import { EventListRelationFilter } from "../event/EventListRelationFilter";
import { StringFilter } from "../../util/StringFilter";

export type DeviceWhereInput = {
  deviceId?: StringNullableFilter;
  deviceName?: StringNullableFilter;
  events?: EventListRelationFilter;
  id?: StringFilter;
};
