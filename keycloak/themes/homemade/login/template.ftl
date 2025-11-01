<#macro registeredLayout bodyClass="" displayInfo=false displayMessage=true displayRequiredFields=false>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="robots" content="noindex, nofollow">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <#if properties.meta?has_content>
        <#list properties.meta?split(' ') as meta>
            <meta name="${meta?split('==')[0]}" content="${meta?split('==')[1]}"/>
        </#list>
    </#if>

    <title>${msg("loginTitle",(realm.displayName!''))}</title>

    <#if properties.favicons?has_content>
        <#list properties.favicons?split(' ') as favicon>
            <link rel="${favicon?split('==')[0]}" href="${url.resourcesPath}/${favicon?split('==')[1]}">
        </#list>
    </#if>

    <link rel="stylesheet" href="${url.resourcesPath}/css/homemade.css">
</head>

<body class="${bodyClass} min-h-screen bg-gradient-to-br from-primary-50 to-secondary-50 flex items-center justify-center p-4">
    <div class="w-full max-w-md">
        <div class="bg-white rounded-lg shadow-lg p-8">
            <#-- Header with branding -->
            <div class="text-center mb-6">
                <h1 class="text-3xl font-bold text-neutral-900 mb-2">Homemade</h1>
                <#if realm.displayName??>
                    <p class="text-sm text-neutral-600">${realm.displayName}</p>
                </#if>
            </div>

            <#-- Display messages -->
            <#if displayMessage && message?has_content && (message.type != 'warning' || !isAppInitiatedAction??)>
                <div class="mb-4 p-4 rounded-md ${message.type = 'success'}?then('bg-success-100 text-success-800 border border-success-300', message.type = 'warning'}?then('bg-warning-100 text-warning-800 border border-warning-300', message.type = 'error'}?then('bg-error-100 text-error-800 border border-error-300', 'bg-neutral-100 text-neutral-800 border border-neutral-300'))">
                    <#if message.type = 'success'><span class="font-semibold">Success:</span> </#if>
                    <#if message.type = 'warning'><span class="font-semibold">Warning:</span> </#if>
                    <#if message.type = 'error'><span class="font-semibold">Error:</span> </#if>
                    ${kcSanitize(message.summary)?no_esc}
                </div>
            </#if>

            <#-- Main content area -->
            <#nested "form">

            <#-- Info section -->
            <#if displayInfo>
                <div class="mt-6 p-4 bg-neutral-50 rounded-md border border-neutral-200">
                    <#nested "info">
                </div>
            </#if>
        </div>

        <#-- Footer with additional links -->
        <div class="mt-4 text-center text-sm text-neutral-600">
            <#nested "socialProviders">
        </div>
    </div>
</body>
</html>
</#macro>
