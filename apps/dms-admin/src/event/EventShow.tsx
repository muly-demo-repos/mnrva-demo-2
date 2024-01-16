import * as React from "react";
import {
  Show,
  SimpleShowLayout,
  ShowProps,
  DateField,
  ReferenceField,
  TextField,
} from "react-admin";
import { DEVICE_TITLE_FIELD } from "../device/DeviceTitle";

export const EventShow = (props: ShowProps): React.ReactElement => {
  return (
    <Show {...props}>
      <SimpleShowLayout>
        <DateField source="createdAt" label="Created At" />
        <ReferenceField label="device" source="device.id" reference="Device">
          <TextField source={DEVICE_TITLE_FIELD} />
        </ReferenceField>
        <TextField label="eventType" source="eventType" />
        <TextField label="humidity" source="humidity" />
        <TextField label="ID" source="id" />
        <TextField label="temperature" source="temperature" />
        <TextField label="timestamp" source="timestamp" />
        <DateField source="updatedAt" label="Updated At" />
      </SimpleShowLayout>
    </Show>
  );
};
