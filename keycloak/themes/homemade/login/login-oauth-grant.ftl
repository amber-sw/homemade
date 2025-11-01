<#import "template.ftl" as layout>
<@layout.registeredLayout bodyClass="" displayInfo=false displayMessage=true; section>
    <#if section = "form">
        <div id="kc-oauth" class="mb-6">
            <h2 class="text-xl font-semibold text-neutral-900 mb-4">
                <#if client.name?has_content>
                    ${msg("oauthGrantTitle",advancedMsg(client.name))}
                <#else>
                    ${msg("oauthGrantTitle",client.clientId)}
                </#if>
            </h2>

            <p class="text-neutral-700 mb-4">
                ${msg("oauthGrantRequest")}
            </p>

            <#if oauth.clientScopesRequested??>
                <p class="text-sm text-neutral-600 mb-3">
                    <#if client.name?has_content>
                        ${msg("oauthGrantInformation",advancedMsg(client.name))}
                    <#else>
                        ${msg("oauthGrantInformation",client.clientId)}
                    </#if>
                </p>

                <div class="mb-6 p-4 bg-neutral-50 rounded-md border border-neutral-200">
                    <ul class="space-y-2">
                        <#list oauth.clientScopesRequested as clientScope>
                            <li class="flex items-start">
                                <svg class="w-5 h-5 text-primary-500 mr-2 mt-0.5 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path>
                                </svg>
                                <div>
                                    <div class="text-sm font-medium text-neutral-900">
                                        ${advancedMsg(clientScope.consentScreenText)}
                                    </div>
                                    <#if clientScope.dynamicScopeParameter??>
                                        <div class="text-xs text-neutral-600 mt-1">
                                            <span class="font-medium">${msg("oauthGrantDynamicScopeParameter")}:</span> <span class="font-mono bg-neutral-100 px-1.5 py-0.5 rounded">${clientScope.dynamicScopeParameter}</span>
                                        </div>
                                    </#if>
                                </div>
                            </li>
                        </#list>
                    </ul>
                </div>
            </#if>

            <form class="space-y-3" action="${url.oauthAction}" method="POST">
                <input type="hidden" name="code" value="${oauth.code}">

                <div class="flex gap-3">
                    <button
                        type="submit"
                        id="kc-login"
                        name="accept"
                        class="flex-1 bg-primary-500 hover:bg-primary-600 text-white font-semibold py-2.5 px-4 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:ring-offset-2"
                    >
                        ${msg("doYes")}
                    </button>

                    <button
                        type="submit"
                        id="kc-cancel"
                        name="cancel"
                        class="flex-1 bg-neutral-200 hover:bg-neutral-300 text-neutral-800 font-semibold py-2.5 px-4 rounded-md transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-neutral-400 focus:ring-offset-2"
                    >
                        ${msg("doNo")}
                    </button>
                </div>
            </form>
        </div>
    </#if>
</@layout.registeredLayout>
