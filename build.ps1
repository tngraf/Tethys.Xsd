# -----------------------------------------
# Build project
# SPDX-License-Identifier: Apache-2.0
# SPDX-FileCopyrightText: 2021-2022 T. Graf
# -----------------------------------------

dotnet restore

dotnet build --configuration Release --no-restore
