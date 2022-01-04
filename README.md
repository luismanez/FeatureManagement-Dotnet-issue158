# FeatureManagement-Dotnet-issue158
This is a minimal asp.net core app using FeatureManagement package with Azure App Configuration, reproducing the issue 158 (https://github.com/microsoft/FeatureManagement-Dotnet/issues/158).

To reproduce the issue:
  - Create the App Configuration Feature flags as described in the issue
  - Clone the repo
  - Update AppSettings.json file with the proper connection string (and FilterKey and FilterLabel, if you has created the Feature Flags with different names)

Run the project and call the endpoint:

```
https://localhost:44340/api/features
```

As per my tests, the FeatureFlag BestDeals for Contoso prefix and UAT label, appears as Disabled (however, that feature is enabled in the portal).
