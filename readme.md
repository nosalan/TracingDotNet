Run all three projects
Make request to http://localhost:5001/api
 - you may attach the traceparent header, e.g. 00-0001651916cd43dd8448eb211c80319c-b7ad6b7169203331-00
Observe how trace id and parent id propagate

The projects are also integrated with new relic, in order for integration to work edit the launchSettings.json files
and provide path to CORECLR_PROFILER_PATH and CORECLR_NEWRELIC_HOME
It may be improved to remove the absolute paths